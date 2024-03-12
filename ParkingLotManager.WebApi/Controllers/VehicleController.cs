using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class VehicleController : ControllerBase
{
    [HttpGet("v1/vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync([FromServices] AppDataContext ctx)
    {
        try
        {
            var vehicles = await ctx.Vehicles.AsNoTracking().ToListAsync();
            if (vehicles == null)
                return BadRequest(new { message = "01EX1000 - Request could not be processed. Please try another time" });
            return Ok(vehicles);
        }
        catch
        {
            return StatusCode(500, new { message = "01EX1001 falha interna no servidor" });
        }
    }

    [HttpGet("v1/vehicles/{licentePlate:string}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByLicensePlateAsync([FromServices] AppDataContext ctx, [FromRoute] string licentePlate)
    {
        try
        {
            var vehicle = await ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licentePlate);
            if (vehicle is null)
                return NotFound(new { message = "01EX1002 - License plate not found." });

            return Ok(vehicle);
        }
        catch
        {
            return StatusCode(500, new { message = "01EX1003 - Internal server error" });
        }
    }

    [HttpPost("v1/vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterVehicleAsync(
        [FromServices] AppDataContext ctx,
        [FromBody] Vehicle model)
    {
        try
        {
            await ctx.Vehicles.AddAsync(model);
            await ctx.SaveChangesAsync();
            return Created($"v1/vehicles/{model.LicensePlate}", model);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { message = "01EX2000 - Could not register vehicle" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "01EX2001 - Internal server error" });
        }

    }

    [HttpPut("v1/vehicles/{licensePlate:string}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateVehicle(
            [FromServices] AppDataContext ctx,
            [FromRoute] string licensePlate,
            [FromBody] Vehicle model
        )
    {
        try
        {
            var vehicle = await ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new { message = "01EX3000 - Vehicle not found." });

            //vehicle.CompanyName = model.CompanyName ?? vehicle.CompanyName;






            return Ok(model);
        }
        catch
        {
            return StatusCode(500, new { message = "01EX3001 - Internal server error" });
        }
    }

    public async Task<IActionResult> DeleteVehicle(
            [FromServices] AppDataContext ctx,
            [FromRoute] string licensePlate
        )
    {
        try
        {
            var vehicle = await ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new { message = "01EX4000 - Vehicle not found." });

            ctx.Vehicles.Remove(vehicle);
            await ctx.SaveChangesAsync();

            return Ok(vehicle);
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "01EX4001 - Internal server error" });
        }
    }
}
