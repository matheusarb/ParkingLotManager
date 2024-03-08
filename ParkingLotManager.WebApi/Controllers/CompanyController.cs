using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class CompanyController : ControllerBase
{
    [HttpGet("v1/companies")]
    public IActionResult Get([FromServices] AppDataContext ctx)
    {
        var companies = ctx.Companies.AsNoTracking().ToList();
        return Ok(companies);
    }
}
