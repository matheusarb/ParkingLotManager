using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class CompanyController : ControllerBase
{
    private readonly AppDataContext _ctx;

    public CompanyController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet("v1/companies")]
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
    public async Task<IActionResult> GetAsyncByName([FromRoute] string name)
    {
        try
        {
            var company = await _ctx.Companies.FirstOrDefaultAsync(x => x.Name == name);
            if (company == null)
                return NotFound(new { message = "05EX5002 - Company not foun." });

            return Ok(new ResultViewModel<Company>(company));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Company>("05EX5003 - Internal server error"));
        }
    }

    [HttpPost("v1/companies")]
    public async Task<IActionResult> RegisterCompanyAsync([FromBody] RegisterCompanyViewModel viewModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<RegisterCompanyViewModel>(ModelState.GetErrors()));
        try
        {
            return null;
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05EX5001 - Internal server error"));            
        }
    }
}
