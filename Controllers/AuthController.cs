using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {

        if (!ModelState.IsValid)
            return BadRequest("Preencha todos os campos corretamente");

        var token = AuthService.Login(login.email, login.pass);

        if (!string.IsNullOrWhiteSpace(token))
        {
            return Ok(new { jwt = token });
        }
        else
        {
            return BadRequest("Email ou Senha inválidos");
        }
    }

    [HttpPost("signin")]
    public IActionResult Signin([FromBody] UserDto user)
    {
        if (!ModelState.IsValid)
            return BadRequest("Preencha os campos corretamente");

        var newUser = AuthService.Signin(user);

        return Created("", newUser);
    }
}
