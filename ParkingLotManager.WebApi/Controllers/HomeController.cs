using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ParkingLotManager.WebApi.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public IActionResult CheckStatus()
    { 
        return Ok(new
        {
            message = "API online"
        });
    }
}
