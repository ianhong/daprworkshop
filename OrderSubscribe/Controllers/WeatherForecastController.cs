using Dapr;
using OrderEvent;
using Microsoft.AspNetCore.Mvc;

namespace OrderSubscribe.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [Topic("neworderevent", "neworder")]
    [HttpPost("/imported")]
    public dynamic imported([FromBody] NewOrderEvent orderevent)
    {
        _logger.LogInformation($"Order {orderevent.OrderId} is {orderevent.OrderDate}");
        return orderevent;
    }
}
