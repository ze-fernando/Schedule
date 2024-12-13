using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Schedule.Services;
using Schedule.Dtos;
using Schedule.Models;
using System.Security.Claims;
using System;

namespace Schedule.Controllers;

[Authorize]
[ApiController]
[Route("/api/")]
public class ScheduleController(AppointmentService service) : ControllerBase
{
    private readonly AppointmentService _service = service;

    [HttpPost]
    public IActionResult Create([FromBody] AppointmentDto apDto)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Você precisa estar logado");

        Appointment newAppointment = _service.CreateAppointment(apDto, userId);

        return Created("", new { data = newAppointment });
    }

    [HttpGet]
    public IActionResult Read()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Você precisa estar logado");
            
        ICollection<Appointment> aps = _service.ReadAppointment(userId);

        return Ok(new{Appointments = aps});
    }

    [HttpGet("{id}")]
    public IActionResult ReadOnly([FromRoute] int id)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Você precisa estar logado");
        Appointment appointment = _service.ReadOnlyAppointment(userId, id);

        if(appointment != null)
            return Ok(new{Apoointment = appointment});
        
        return BadRequest("Id não encontrado");
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] AppointmentDto apDto)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Você precisa estar logado");

        var appointment = _service.UpdateAppointment(apDto, userId, id);

        if(appointment != null)
            return Ok(new {data = appointment});
        
        return BadRequest("Id não encontrado");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return BadRequest("Você precisa estar logado");

        var status = _service.DeleteAppointment(userId, id);

        if(status)
            return Ok();
        
        return BadRequest("Id não encontrado");
    }
}

