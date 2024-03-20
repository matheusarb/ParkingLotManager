using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Attributes;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Extensions;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System.Data.Common;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class VehicleController : ControllerBase
{
    private readonly AppDataContext _ctx;
    private const string apiKeyName = Configuration.ApiKeyName;

    private VehicleController()
    {        
    }

    public VehicleController(AppDataContext ctx)
        => _ctx = ctx;

    /// <summary>
    /// gets collection of vehicles
    /// </summary>
    /// <returns>collection of vehicles</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/vehicles")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAsync([FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var vehicles = await _ctx.Vehicles.AsNoTracking().ToListAsync();
            if (vehicles == null)
                return BadRequest(new { message = "01EX1000 - Request could not be processed. Please try another time" });

            return Ok(new ResultViewModel<List<Vehicle>>(vehicles));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Vehicle>>("01EX1001 - Internal server error"));
        }
    }

    /// <summary>
    /// gets vehicle by licensePlate
    /// </summary>
    /// <returns>vehicle data by licensePlate</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/vehicles/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetByLicensePlateAsync(
        [FromRoute] string licensePlate,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var vehicle = await _ctx.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle is null)
                return NotFound(new { message = "01EX1002 - License plate not found." });

            return Ok(new ResultViewModel<Vehicle>(vehicle));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX1003 - Internal server error"));
        }
    }

    /// <summary>
    /// gets collection of Ford vehicles
    /// </summary>
    /// <returns>collection of Ford vehicles</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("v1/vehicles/ford")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetIfBrandIsFordAsync()
    {
        try
        {
            var fordVehicles = await _ctx.Vehicles.Where(x => x.Brand == "Ford").AsNoTracking().ToListAsync();
            if (fordVehicles == null)
                return NotFound(new ResultViewModel<string>("01EX1004 - Request could not be processed. Please try again another time"));

            return Ok(new ResultViewModel<List<Vehicle>>(fordVehicles));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX1005 - Internal server error"));
        }
    }

    /// <summary>
    /// registers a new vehicle
    /// </summary>
    /// <remarks>
    /// {"company":{"cnpj":{"cnpjNumber":"string"},"address":{"street":"string","city":"string","zipCode":"string"}},"licensePlate":"strings","brand":"string","model":"string","color":"string","type":1,"companyName":"string"}
    /// </remarks>
    /// <returns>new vehicle data</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost("v1/vehicles")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterVehicleViewModel viewModel,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<RegisterVehicleViewModel>(ModelState.GetErrors()));
        try
        {
            var vehicle = new Vehicle();
            var createdVehicle = vehicle.Create(viewModel);
            await _ctx.Vehicles.AddAsync(createdVehicle);
            await _ctx.SaveChangesAsync();

            return Created($"vehicles/v1/{createdVehicle.LicensePlate}", new ResultViewModel<Vehicle>(createdVehicle));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX2000 - Could not register vehicle"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX2001 - Internal server error"));
        }

    }

    /// <summary>
    /// updates data of a registered vehicle
    /// </summary>
    /// <returns>updated data of vehicle</returns>
    /// <remarks>
    /// {"company":{"cnpj":{"cnpjNumber":"string"},"address":{"street":"string","city":"string","zipCode":"string"}},"licensePlate":"strings","brand":"string","model":"string","color":"string","type":1,"companyName":"string"}
    /// </remarks>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("v1/vehicles/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromRoute] string licensePlate,
        [FromBody] UpdateVehicleViewModel viewModel,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        if (viewModel.CheckIfAllEmpty(viewModel))
            return BadRequest(new ResultViewModel<UpdateVehicleViewModel>("Cannot update infos if all values are empty"));

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Vehicle>(ModelState.GetErrors()));
        try
        {
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new ResultViewModel<Vehicle>("01EX3000 - Vehicle not found."));

            vehicle.Update(viewModel);
            _ctx.Update(vehicle);
            await _ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<Vehicle>(vehicle));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Vehicle>("01EX3001 - Internal server error"));
        }
    }

    /// <summary>
    /// deletes vehicle by licensePlate
    /// </summary>
    /// <returns>data of deleted vehicle</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("v1/vehicles/{licensePlate}")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(
        [FromRoute] string licensePlate,
        [FromQuery(Name = apiKeyName)] string apiKeyName)
    {
        try
        {
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
            if (vehicle == null)
                return NotFound(new ResultViewModel<Vehicle>("01EX4000 - Vehicle not found."));

            _ctx.Vehicles.Remove(vehicle);
            await _ctx.SaveChangesAsync();

            return Ok(new ResultViewModel<Vehicle>(vehicle));
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "01EX4001 - Internal server error" });
        }
    }
}
