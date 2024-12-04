using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginDto login)
    {
        if (string.IsNullOrWhiteSpace(login.mail) || string.IsNullOrWhiteSpace(login.pass))
            return BadRequest("Preencha todos os campos");


        if (!Helpers.ValidMail(login.mail))
            return BadRequest("Formato de mail inválido");

        var token = LoginService.Login(login.mail, login.pass);

        if (token)
        {
            return Ok(new { jwt = token });
        }
        else
        {
            return BadRequest("Email ou Senha inválidos");
        }
    }
}
