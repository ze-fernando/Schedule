using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Authorize]
[Route("api")]
public class ScheduleController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(ScheduleDto schedule)
    {
        if (!ModelState.IsValid)
            return BadRequest("Preencha todos os campos corretamente");

        var newSchedule = ScheduleService.CreateSchedule(schedule);

        return Created("", new { schedule = newSchedule });
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

