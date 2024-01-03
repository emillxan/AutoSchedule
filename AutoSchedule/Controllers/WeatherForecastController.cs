using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.BLL.DTOs.Lessons;
using AutoSchedule.BLL.Logic;
using AutoSchedule.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController(IScheduleBuilder priority,
        ILessonService lessonService,
        ILessonDTOService lessonDTOService) : ControllerBase
    {
        private readonly IScheduleBuilder _priority = priority;
        private readonly ILessonService _lessonService = lessonService;
        private readonly ILessonDTOService _lessonDTOService = lessonDTOService;
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

/*        [HttpPost(Name = "Create")]
        public IActionResult Create()
        {

            var result = _priority.Create();
            return Ok(result);
        }*/


        [HttpGet(Name = "GetLessonsBySquadId")]
        public IActionResult GetLessonsBySquadId(int id)
        {

            var result = _lessonService.GetBySquadId(id);
            List<LessonDTO> list = new List<LessonDTO>();
            foreach (var item in result.Result.Data)
            {
                list.Add(_lessonDTOService.GetDTOByLesson(item).Result.Data);
            }
            return Ok(list);
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
