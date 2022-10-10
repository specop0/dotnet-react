using System;
using System.Collections.Generic;
using System.Linq;
using DotNetReact.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    [HttpGet]
    [SwaggerOperation(
        OperationId = "getWeatherForecast",
        Summary = "Gets the weather forecast.",
        Description = "Returns the weather forecast.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the weather forecast.", typeof(IEnumerable<WeatherForecast>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "For testing purposes.", typeof(ErrorResponse))]
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
