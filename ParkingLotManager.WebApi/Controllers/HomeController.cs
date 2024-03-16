using Microsoft.AspNetCore.Mvc;
using ParkingLotManager.WebApi.Attributes;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels;
using System.Text.Json.Serialization;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("status-check")]
    public IActionResult CheckStatus()
    {
        try
        {
            return Ok(new { message = "API is online" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "00EX0000 - Internal server error" });
        }        
    }

    [HttpGet("")]
    [ApiKey]
    public IActionResult ValidateApiKey()
    {
        return Ok(new ResultViewModel<string>("Valid ApiKey", null));
    }
}
