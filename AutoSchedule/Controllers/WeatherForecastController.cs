using AutoSchedule.BLL.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IPriority priority) : ControllerBase
    {
        private readonly IPriority _priority = priority;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet(Name = "Test")]
        public IActionResult GetTest()
        {

            var result = _priority.StartR();
            return Ok(result);
        }

        [HttpPost(Name = "Create")]
        public IActionResult Create()
        {

            var result = _priority.Create();
            return Ok(result);
        }

        /*        [HttpGet(Name = "GetWeatherForecast")]
                public IEnumerable<WeatherForecast> Get()
                {


                    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    })
                    .ToArray();
                }*/


    }
}
