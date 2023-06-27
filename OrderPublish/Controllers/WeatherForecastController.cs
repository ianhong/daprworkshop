using Microsoft.AspNetCore.Mvc;
using Dapr.Client;
using OrderEvent;

namespace OrderPublish.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DaprClient _daprClient;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DaprClient daprClient)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public bool Get()
    {
        var newevent = new NewOrderEvent()
        {
            OrderId = Guid.NewGuid().ToString(),
            OrderDate = DateTime.Now.ToString()
        };

        _daprClient.PublishEventAsync<NewOrderEvent>
        (
            "neworderevent",
            "neworder",
            newevent,
            new Dictionary<string, string>() { { "country", "CA" } } 

        ).GetAwaiter().GetResult();

        return true;
    }
}
