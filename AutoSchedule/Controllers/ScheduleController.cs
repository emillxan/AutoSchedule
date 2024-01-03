using AutoSchedule.BLL.Logic;
using AutoSchedule.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ScheduleController(IScheduleBuilder scheduleBuilder) : Controller
{
    private readonly IScheduleBuilder _scheduleBuilder = scheduleBuilder;
    [HttpGet]
    public IActionResult GetTest()
    {

        var result = _scheduleBuilder.StartR();
        return Ok(result);
    }


}
