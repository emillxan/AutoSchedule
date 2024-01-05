/*using AutoSchedule.BLL.CRUD.Subjects;
using AutoSchedule.Domain.DTOs.Squads;
using AutoSchedule.Domain.DTOs.Subjects;
using AutoSchedule.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SubjectController(ISubjectService subjectService) : Controller
{
    private readonly ISubjectService _subjectService = subjectService;

    [HttpPost]
    public IActionResult Create(CreateSubjectDTO model)
    {
        var responce = _subjectService.Create(model);
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var responce = _subjectService.GetAll().Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        var responce = _subjectService.GetById(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpPatch]
    public IActionResult Edit(Subject model)
    {
        var responce = _subjectService.Edit(model).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var responce = _subjectService.Delete(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }
}
*/