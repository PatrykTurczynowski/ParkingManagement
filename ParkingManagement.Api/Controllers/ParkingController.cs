using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Application.DTOs;
using ParkingManagement.Domain.Contracts;

namespace ParkingManagement.Api.Controllers;

[ApiController]
[Route("parking")]
public class ParkingController(IParkingService parkingService) : ControllerBase
{
    private readonly IParkingService _parkingService = parkingService;

    /// <summary>
    /// Gets the current parking status, including the number of available and occupied spaces.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ParkingStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetParkingStatus()
    {
        var result = await _parkingService.GetParkingStatusAsync();
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, new { result.Message });
        }
        return Ok(result.Data);
    }

    /// <summary>
    /// Parks a vehicle in the first available space.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ParkVehicleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] ParkVehicleRequest request)
    {
        var result = await _parkingService.ParkVehicleAsync(request);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, new { result.Message });
        }
        return Ok(result.Data);
    }

    /// <summary>
    /// Processes a vehicle exit and calculates the parking charge.
    /// </summary>
    [HttpPost]
    [Route("exit")]
    [ProducesResponseType(typeof(ExitVehicleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] ExitVehicleRequest request)
    {
        var result = await _parkingService.ExitVehicleAsync(request);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, new { result.Message });
        }
        return Ok(result.Data);
    }
}
