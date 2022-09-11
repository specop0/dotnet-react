using System;
using System.Collections.Generic;
using System.Linq;
using DotNetReact.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetReact.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    /// <summary>
    /// Gets the weather forecast.
    /// </summary>
    /// <response code="200">Returns the weather forecast.</response>
    /// <response code="400">For testing purposes.</response>
    [HttpGet]
    [OperationId("getWeatherForecast")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Get()
    {
        var isError = Random.Shared.Next(2) == 0;

        if (isError)
        {
            return this.BadRequest(new ErrorResponse
            {
                Error = new Error
                {
                    Code = "400",
                    Message = "Test error message."
                }
            });
        }

        return this.Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray());
    }
}
