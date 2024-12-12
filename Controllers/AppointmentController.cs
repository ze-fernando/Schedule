using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Schedule.Services;
using Schedule.Dtos;
using Schedule.Models;
using System.Security.Claims;

namespace Schedule.Controllers;

[Authorize]
[ApiController]
[Route("/api/")]
public class ScheduleController(AppointmentService service) : ControllerBase
{
    private readonly AppointmentService _service = service;

    [HttpPost]
    public IActionResult Create([FromBody] AppointmentDto appointment)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return BadRequest("Você precisa estar logado");

        Appointment newAppointment = _service.CreateAppointment(appointment, userId);

        return Created("", new { data = newAppointment });
    }

    [HttpGet]
    public IActionResult Read()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        ICollection<Appointment> aps = _service.ReadAppointment(userId);

        return Ok(new{Appointments = aps});
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

