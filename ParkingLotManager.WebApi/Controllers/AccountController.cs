using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly TokenService _tokenService;

    public AccountController(TokenService tokenService, AppDataContext ctx)
    {
        _ctx = ctx;
        _tokenService = tokenService;
    }

    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
        try
        {
            //var user = _ctx.Users.AsNoTracking().Include(x => x.)
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(null);
    }

    [Authorize(Roles = "user")]
    [HttpGet("v1/accounts")]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var users = await _ctx.Users.AsNoTracking().ToListAsync();
            if(users == null)
                return BadRequest(new ResultViewModel<List<User>>("06EX6001 - Request could not be processed. Please try another time"));
            return Ok(new ResultViewModel<List<User>>(users));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<User>>("06EX6002 - Internal server error"));
        }
    }


    [Authorize(Roles = "admin")]
    [Authorize(Roles = "user")]
    [HttpGet("v1/accounts/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return BadRequest(new ResultViewModel<User>("06EX6003 - User not found"));
            return Ok(new ResultViewModel<User>(user));

        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<User>("06EX6004 - Internal server error"));
        }
    }

    [HttpPost("v1/accounts")]
    public async Task<IActionResult> Create([FromBody] CreateUserViewModel viewModel)
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
                user.Email, password
            }));
        }
        catch (DbException)
        {
            return StatusCode(400, new ResultViewModel<List<Company>>("06EX6005 - Email is already in use"));

        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("06EX5006 - Internal server error"));
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/admin")]
    public IActionResult GetAdmin() => Ok(User.Identity.Name);
}
