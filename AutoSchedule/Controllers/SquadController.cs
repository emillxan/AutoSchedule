using AutoSchedule.BLL.CRUD.Squads;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Domain.DTOs.Cabinets;
using AutoSchedule.Domain.DTOs.Squads;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SquadController(ISquadService squadService) : Controller
{
    private readonly ISquadService _squadService = squadService;

    [HttpPost]
    public IActionResult Create(CreateSquadDTO model)
    {
        var responce = _squadService.Create(model);
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var responce = _squadService.GetAll().Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        var responce = _squadService.GetById(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpPatch]
    public IActionResult Edit(Squad model)
    {
        var responce = _squadService.Edit(model).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var responce = _squadService.Delete(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }
}
