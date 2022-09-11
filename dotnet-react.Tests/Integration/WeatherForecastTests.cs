using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetReact.Model;
using NUnit.Framework;

namespace DotNetReact.Tests.Integration;

public class WeatherForecastTests
{
    public WebApplicationFactory Factory {get;set;}
    public HttpClient Client { get; set; }

    [OneTimeSetUp]
    public void SetUpOnce()
    {
        this.Factory = new WebApplicationFactory();
        this.Client = this.Factory.CreateClient();
    }

    [OneTimeTearDown]
    public void TearDownOnce()
    {
        this.Client.Dispose();
    }

    [Test]
    public async Task TestGetWeatherForecastOk()
    {
        // Arrange

        // Act
        var response = await this.Client.GetAsync("api/WeatherForecast");
        for (var i = 0; i < 10 && response.StatusCode != HttpStatusCode.OK; i++)
        {
            response = await this.Client.GetAsync("api/WeatherForecast");
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var content = await response.Content.ReadAsStringAsync();
        var actual = JsonSerializer.Deserialize<WeatherForecast[]>(content);
        Assert.That(actual!.Length, Is.EqualTo(5));
    }

    [Test]
    public async Task TestGetWeatherForecastBadRequest()
    {
        // Arrange

        // Act
        var response = await this.Client.GetAsync("api/WeatherForecast");
        for (var i = 0; i < 10 && response.StatusCode != HttpStatusCode.BadRequest; i++)
        {
            response = await this.Client.GetAsync("api/WeatherForecast");
        }

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        var content = await response.Content.ReadAsStringAsync();
        var actual = JsonSerializer.Deserialize<ErrorResponse>(content);
        Assert.That(actual!.Error.Code, Is.EqualTo("400"));
        Assert.That(actual!.Error.Message, Is.EqualTo("Test error message."));
    }
}