using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]/")]
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
    public IActionResult Singin([FromBody] UserDto user)
    {


        return Ok();
    }
}
