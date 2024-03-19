﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("home/status-check")]
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

    [HttpGet("home/validate-api-key")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ValidateApiKey()
    {
        return Ok(new ResultViewModel<string>("Valid ApiKey", null));
    }
}
