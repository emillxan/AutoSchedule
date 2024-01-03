using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.BLL.DTOs.Lessons;
using AutoSchedule.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LessonController(ILessonService lessonService,
    ILessonDTOService lessonDTOService) : Controller
{
    private readonly ILessonService _lessonService = lessonService;
    private readonly ILessonDTOService _lessonDTOService = lessonDTOService;

    [HttpGet]
    public IActionResult GetLessonsBySquadId(int id)
    {

        var result = _lessonService.GetBySquadId(id);
        List<LessonDTO> list = new List<LessonDTO>();
        if(result == null)
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
