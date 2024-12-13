using Microsoft.AspNetCore.Mvc;
using Schedule.Services;

namespace Schedule.Controllers;

[ApiController]
[Route("api")]
public class EmailController(EmailService service) : ControllerBase
{
    private readonly EmailService _service = service;

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string token)
    {
        if(string.IsNullOrEmpty(token))
            return BadRequest("Token invalido");
        
        var result = await _service.ConfirmEmail(token);

        if(!result)
            return BadRequest("Token invalido");

        return Ok("Email confimado");
    }
}