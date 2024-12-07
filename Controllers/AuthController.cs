using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        if (!ModelState.IsValid)
            return BadRequest("Preencha os dados corretamente");

        var tokenUser = AuthService.Login(login.email, login.pass);

        if (tokenUser == null)
            return BadRequest("Email ou senha invalidos");

        return Ok(new { Token = tokenUser });
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
