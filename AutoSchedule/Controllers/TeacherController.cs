using AutoSchedule.BLL.CRUD.Teachers;
using AutoSchedule.Domain.DTOs.Subjects;
using AutoSchedule.Domain.DTOs.Teachers;
using AutoSchedule.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TeacherController(ITeacherService teacherService) : Controller
{
    private readonly ITeacherService _teacherService = teacherService;

    [HttpPost]
    public IActionResult Create(CreateTeacherDTO model)
    {
        var responce = _teacherService.Create(model);
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var responce = _teacherService.GetAll().Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        var responce = _teacherService.GetById(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpPatch]
    public IActionResult Edit(Teacher model)
    {
        var responce = _teacherService.Edit(model).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var responce = _teacherService.Delete(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }
}
