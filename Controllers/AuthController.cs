using Microsoft.AspNetCore.Mvc;
using Schedule.Services;
using Schedule.Dtos;

namespace Schedule.Controllers;

[ApiController]
[Route("/api/")]
public class AuthController(AuthService service) : ControllerBase
{
    private readonly AuthService _service = service;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        if (!ModelState.IsValid)
            return BadRequest("Preencha os dados corretamente");

        var tokenUser = _service.Login(login.email, login.password);

        if (tokenUser == null)
            return BadRequest("Email ou senha invalidos");

        return Ok(new { Token = tokenUser });
    }

    [HttpPost("signin")]
    public IActionResult Signin([FromBody] UserDto user)
    {
        if (!ModelState.IsValid)
            return BadRequest("Preencha os campos corretamente");

        var newUser = _service.Signin(user);

        return Created("", newUser);
    }
}
