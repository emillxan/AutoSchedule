/*using AutoSchedule.BLL.CRUD.Cabinets;
using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.BLL.CRUD.Squads;
using AutoSchedule.BLL.DTOs.Lessons;
using AutoSchedule.BLL.Logic;
using AutoSchedule.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers.Home;

public class HomeController : Controller
{
    private readonly IScheduleBuilder _scheduleBuilder;
    private readonly ILessonService _lessonService;
    private readonly ILessonDTOService _lessonDTOService;
    private readonly ISquadService _squadService;
    public HomeController(IScheduleBuilder scheduleBuilder,
        ILessonService lessonService,
        ILessonDTOService lessonDTOService,
        ISquadService squadService)
    {
        _scheduleBuilder = scheduleBuilder;
        _lessonService = lessonService;
        _lessonDTOService = lessonDTOService;
        _squadService = squadService;
    }


    public IActionResult Index()
    {
        var responce = _squadService.GetAll().Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    public IActionResult CreateSchedule()
    {
        var result = _scheduleBuilder.StartR();
        if (result.Any())
        {
            return Ok(result);
        }
        return BadRequest();
    }

    public IActionResult GetLessonsBySquadId()
    {
        var result = _lessonService.GetBySquadId(id);
        List<LessonDTO> list = new List<LessonDTO>();
        if (result.Result.Data == null)
        {
            return BadRequest(result);
        }
        foreach (var item in result.Result.Data)
        {
            list.Add(_lessonDTOService.GetDTOByLesson(item).Result.Data);
        }
        return Ok(list);
    }
}
*/