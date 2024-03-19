﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Attributes;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Services;
using ParkingLotManager.WebApi.ViewModels;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using ParkingLotManager.WebApi.ViewModels.UserViewModels;
using SecureIdentity.Password;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingLotManager.WebApi.Controllers;


[ApiController]
public class AccountController : ControllerBase
{
    private readonly AppDataContext _ctx;

    public AccountController(AppDataContext ctx)
        => _ctx = ctx;

    /// <summary>
    /// login into system and generate Bearer Token
    /// </summary>
    /// <param name="viewModel">email and password</param>
    /// <param name="tokenService">Bearer Token generator</param>
    /// <returns>Bearer Token</returns>
    [HttpPost("v1/accounts/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel, [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
        try
        {
            var user = await _ctx
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == viewModel.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("06EX6000 - Invalid user or password"));
            if (!PasswordHasher.Verify(user.PasswordHash, viewModel.Password))
                return StatusCode(401, new ResultViewModel<string>("06EX6000 - Invalid user or password"));

            try
            {
                // Send token
                var token = tokenService.GenerateToken(user);
                return Ok(new JsonResult(token).Value);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<List<User>>("06EX6001 - Internal server error"));
            }
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<User>>("06EX6001 - Internal server error"));
        }
    }

    /// <summary>
    /// get collection of users
    /// </summary>
    /// <returns>collection of users</returns>
    [HttpGet("v1/accounts")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync([FromQuery(Name = Configuration.ApiKeyName)] string apiKeyName)
    {
        try
        {
            var users = await _ctx.Users.AsNoTracking().ToListAsync();
            if (users == null)
                return BadRequest(new ResultViewModel<List<User>>("06EX6002 - Request could not be processed. Please try another time"));
            return Ok(new ResultViewModel<List<User>>(users));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<User>>("06EX6003 - Internal server error"));
        }
    }

    /// <summary>
    /// get user by id
    /// </summary>
    /// <param name="id">user id</param>
    /// <returns>user</returns>
    [HttpGet("v1/accounts/{id:int}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromQuery(Name = Configuration.ApiKeyName)] string apiKeyName)
    {
        try
        {
            var user = await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return BadRequest(new ResultViewModel<User>("06EX6004 - User not found"));
            return Ok(new ResultViewModel<User>(user));

        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<User>("06EX6005 - Internal server error"));
        }
    }

    /// <summary>
    /// creates a user with no role
    /// </summary>
    /// <param name="viewModel">viewModel to create user</param>
    /// <returns>created user and its Uri</returns>
    [HttpPost("v1/accounts")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserViewModel viewModel, [FromQuery(Name = Configuration.ApiKeyName)] string apiKeyName)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
        try
        {
            var user = new User();
            var password = PasswordGenerator.Generate(25);
            user.Create(viewModel, password);
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return Created($"v1/users/{user.Id}", new ResultViewModel<dynamic>(new
            {
                user.Email,
                password
            }));
        }
        catch (DbException)
        {
            return StatusCode(400, new ResultViewModel<List<Company>>("06EX6006 - Email is already in use"));

        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("06EX5007 - Internal server error"));
        }
    }

    /// <summary>
    /// updates a user by its id
    /// </summary>
    /// <param name="viewModel">viewModel to update user</param>
    /// <param name="id">user id</param>
    /// <returns>updated user</returns>
    [HttpPut("v1/accounts/{id:int}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateUserViewModel viewModel,
        [FromQuery(Name = Configuration.ApiKeyName)] string apiKeyName)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return BadRequest(new ResultViewModel<string>("06EX6004 - Request could not be processed. Please try another time"));

            user.Update(viewModel);
            _ctx.Update(user);
            await _ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<User>(user));
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<string>("06EX6006 - Request could not be processed. Please try another time"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("06EX6007 - Internal server error"));
        }
    }

    /// <summary>
    /// deletes a user by its id
    /// </summary>
    /// <param name="id">user id</param>
    /// <returns>deleted user</returns>
    [HttpDelete("v1/accounts/{id:int}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return BadRequest(new ResultViewModel<string>("06EX6008 - Request could not be processed. Please try another time"));

            _ctx.Remove(user);
            await _ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<User>(user));
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<string>("06EX6009 - Request could not be processed. Please try another time"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>("06EX6010 - Internal server error"));
        }
    }

    /// <summary>
    /// creates a user with admin role
    /// </summary>
    /// <param name="viewModel">viewModel to create admin</param>
    /// <returns>user with admin role</returns>
    [HttpPost("v1/accounts/admin")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAdminAsync([FromBody] CreateUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
        try
        {
            var user = new User();
            var password = PasswordGenerator.Generate(25);
            user.CreateAdmin(viewModel, password);
            var userRole = user.Roles?.Select(x => x.Name);
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return Created($"v1/users/admin/{user.Id}", new ResultViewModel<dynamic>(new
            {
                user.Email,
                password,
                userRole
            }));
        }
        catch (DbException)
        {
            return StatusCode(400, new ResultViewModel<List<Company>>("06EX6006 - Email is already in use"));

        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("06EX5007 - Internal server error"));
        }
    }
}
