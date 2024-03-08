using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class CompanyController : ControllerBase
{
    [HttpGet("v1/companies")]
    public async Task<IActionResult> GetAsync([FromServices] AppDataContext ctx)
    {
        var companies = await ctx.Companies.AsNoTracking().ToListAsync();
        return Ok(companies);
    }
}
