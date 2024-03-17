using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Attributes;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System.Data.Common;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
[ApiKey]
public class CompanyController : ControllerBase
{
    private readonly AppDataContext _ctx;

    public CompanyController(AppDataContext ctx)
        => _ctx = ctx;

    [HttpGet("v1/companies")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync([FromServices] AppDataContext ctx)
    {
        try
        {
            var companies = await ctx.Companies.AsNoTracking().ToListAsync();
            if (companies == null)
                return BadRequest(new { message = "05EX5000 - Request could not be processed. Please try another time" });

            return Ok(new ResultViewModel<List<Company>>(companies));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5001 - Internal server error"));
        }
    }

    [HttpGet("v1/companies/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsyncByName([FromRoute] string name)
    {
        try
        {
            var company = await _ctx.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
            if (company == null)
                return NotFound(new { message = "05EX5002 - Company not found." });

            return Ok(new ResultViewModel<Company>(company));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Company>("05EX5003 - Internal server error"));
        }
    }

    [HttpPost("v1/companies")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCompanyViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<RegisterCompanyViewModel>(ModelState.GetErrors()));
        try
        {
            var company = new Company();
            company.Create(viewModel);
            await _ctx.Companies.AddAsync(company);
            await _ctx.SaveChangesAsync();

            return Created($"v1/companies/{company.Name}", new ResultViewModel<Company>(company));
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5002 - Could not register company"));

        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5003 - Internal server error"));
        }
    }

    [HttpPut("v1/companies/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] string name, [FromBody] UpdateCompanyViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<UpdateCompanyViewModel>(ModelState.GetErrors()));
        
        try
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(x => x.Name == name);
            if (company == null)
                return NotFound(new ResultViewModel<UpdateCompanyViewModel>("05EX5007 - Company not found"));
            company.Update(viewModel, viewModel.Address);
            _ctx.Update(company);
            await _ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<Company>(company));
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5007 - Could not update company"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5008 - Internal server error"));
        }
    }

    [HttpDelete("v1/companies/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] string name)
    {
        try
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(x => x.Name == name);
            if(company == null)
                return BadRequest(new ResultViewModel<Company>("05EX5005 - Company not found"));
            
            _ctx.Remove(company);
            await _ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<Company>(company));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5006 - Internal server error"));
        }
    }
}
