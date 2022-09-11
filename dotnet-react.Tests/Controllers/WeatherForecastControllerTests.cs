using DotNetReact.Controllers;
using DotNetReact.Model;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace DotNetReact.Tests.Controllers;

public class WeatherForecastControllerTests
{
    [Test]
    public void TestGetWeatherForecastOk()
    {
        // Arrange
        var testee = new WeatherForecastController();

        // Act
        var result = testee.Get();
        for (var i = 0; i < 10 && result is not OkObjectResult; i++)
        {
            result = testee.Get();
        }

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.InstanceOf<WeatherForecast[]>());
        var actual = (WeatherForecast[])okResult.Value;
        Assert.That(actual!.Length, Is.EqualTo(5));
    }

    [Test]
    public void TestGetWeatherForecastBadRequest()
    {
        // Arrange
        var testee = new WeatherForecastController();

        // Act
        var result = testee.Get();
        for (var i = 0; i < 10 && result is not BadRequestObjectResult; i++)
        {
            result = testee.Get();
        }

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = (BadRequestObjectResult)result;
        Assert.That(badRequestResult.Value, Is.InstanceOf<ErrorResponse>());
        var actual = (ErrorResponse)badRequestResult.Value;
        Assert.That(actual!.Error.Code, Is.EqualTo("400"));
        Assert.That(actual!.Error.Message, Is.EqualTo("Test error message."));
    }
}