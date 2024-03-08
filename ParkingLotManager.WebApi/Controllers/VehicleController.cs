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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("v1/vehicles/{licentePlate}")]
    public async Task<IActionResult> GetByLicensePlateAsync([FromServices] AppDataContext ctx, [FromRoute] string licentePlate)
    {
        var vehicle = await ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licentePlate);
        if (vehicle is null)
            return NotFound(new { message = "License plate not found." });

        return Ok(vehicle);
    }

    [HttpPost("v1/vehicle")]
    public async
}
