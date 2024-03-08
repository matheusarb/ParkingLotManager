using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class VehicleController : ControllerBase
{
    [HttpGet("v1/vehicles")]
    public async Task<IActionResult> GetAsync([FromServices] AppDataContext ctx)
    {
        var vehicles = await ctx.Vehicles.AsNoTracking().ToListAsync();
        return Ok(vehicles);
    }
}
