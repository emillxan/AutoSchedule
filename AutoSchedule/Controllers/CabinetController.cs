/*using AutoSchedule.BLL.CRUD.Cabinets;
using AutoSchedule.Domain.DTOs.Cabinets;
using AutoSchedule.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CabinetController(ICabinetService cabinetService) : Controller
{
    private readonly ICabinetService _cabinetService = cabinetService;

    [HttpPost]
    public IActionResult Create(CreateCabinetDTO model)
    {
        var responce = _cabinetService.Create(model);
        if(responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var responce = _cabinetService.GetAll().Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        var responce = _cabinetService.GetById(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpPatch]
    public IActionResult Edit(Cabinet model)
    {
        var responce = _cabinetService.Edit(model).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var responce = _cabinetService.Delete(id).Result.Data;
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest();
    }
}
*/