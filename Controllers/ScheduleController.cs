using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Schedule.Services;
using Schedule.Dtos;

namespace Schedule.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class ScheduleController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(ScheduleDto schedule)
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult Read()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult ReadOnly(int id)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}

