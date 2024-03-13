using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;

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

            return Ok(new ResultViewModel<List<Vehicle>>(vehicles));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Vehicle>>("01EX1001 falha interna no servidor"));
        }
    }

    [HttpGet("v1/vehicles/{licensePlate}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByLicensePlateAsync([FromServices] AppDataContext ctx, [FromRoute] string licensePlate)
    {
        try
        {
            var vehicle = await ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle is null)
                return NotFound(new { message = "01EX1002 - License plate not found." });

            return Ok(new ResultViewModel<Vehicle>(vehicle));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Vehicle> ("01EX1003 - Internal server error"));
        }
    }

    [HttpPost("v1/vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterVehicleAsync([FromServices] AppDataContext ctx, [FromBody] RegisterVehicleViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<RegisterVehicleViewModel>(ModelState.GetErrors()));
        try
        {
            var newVehicle = new Vehicle();
            newVehicle.Create(viewModel, newVehicle);
            await ctx.Vehicles.AddAsync(newVehicle);
            await ctx.SaveChangesAsync();

            return Created($"vehicles/v1/{newVehicle.LicensePlate}", new ResultViewModel<Vehicle>(newVehicle));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX2000 - Could not register vehicle"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX2001 - Internal server error"));
        }

    }

    [HttpPut("v1/vehicles/{licensePlate}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateVehicle([FromServices] AppDataContext ctx, [FromRoute] string licensePlate, [FromBody] UpdateVehicleViewModel viewModel)
    {
        if (viewModel.CheckIfAllEmpty(viewModel))
            return BadRequest(new ResultViewModel<UpdateVehicleViewModel>("Cannot update infos if all values are empty"));

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Vehicle>(ModelState.GetErrors()));
        try
        {
            var vehicle = await ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new ResultViewModel<Vehicle>("01EX3000 - Vehicle not found."));

            vehicle.Update(viewModel, vehicle);
            ctx.Update(vehicle);
            await ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<Vehicle>(vehicle));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX3001 - Internal server error"));
        }
    }

    [HttpDelete("v1/vehicles/{licensePlate}")]
    public async Task<IActionResult> DeleteVehicle([FromServices] AppDataContext ctx,[FromRoute] string licensePlate)
    {        
        try
        {
            var vehicle = await ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new ResultViewModel<Vehicle>( "01EX4000 - Vehicle not found." ));

            ctx.Vehicles.Remove(vehicle);
            await ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<Vehicle>(vehicle));
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "01EX4001 - Internal server error" });
        }
    }
}
