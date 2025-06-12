using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "PostWeatherForecast")]
    public string Post()
    {
        return "helloworkd";
    }

    // public string Index()
    // {
    //     return "Hello World!";
    // }

    // [HttpGet(Name = "helloWorld")]
    // public ViewResult Get()
    // {
    //     ViewResult ah = new ViewResult();
    //     ah.StatusCode = 200;
    //     return ah;

    //     // return "Hello world";
    // }
}
