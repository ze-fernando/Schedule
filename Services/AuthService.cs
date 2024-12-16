using Schedule.Services;
using Schedule.Entities;
using Schedule.Models;
using Schedule.Dtos;
using DotNetEnv;

namespace Schedule.Services;

public class AuthService(AppDbContext context, EmailService service)
{
    private readonly AppDbContext _context = context;
    private readonly EmailService _service = service;

    public string? Login(string email, string pass)
    {

        User? user = _context.Users?.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return null;
        else if (!BCrypt.Net.BCrypt.Verify(pass, user.Password))
            return null;

        else
        {
            var token = TokenService.Generate(email, user.Id);

            return token;
        }

    }

    public async Task<User> SigninAsync(UserDto user)
    {
        var newUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 12),
            Token = Guid.NewGuid().ToString()
        };
        
        var confirmationLink = $"{Settings.BaseUrl}/api/confirm-email?token={newUser.Token}";

        var emailBody = $@"
            <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h1>Bem-vindo, {newUser.FirstName}!</h1>
                    <p>Obrigado por se registrar na nossa aplicação. Por favor, confirme o seu email clicando no botão abaixo:</p>
                    <a href='{confirmationLink}' 
                    style='display: inline-block; padding: 10px 20px; font-size: 16px; color: #fff; background-color: #007BFF; text-decoration: none; border-radius: 5px;'>
                    Clique aqui para confirmar o email
                    </a>
                    <p>Se o botão acima não funcionar, copie e cole o link abaixo no seu navegador:</p>
                    <p><a href='{confirmationLink}'>{confirmationLink}</a></p>
                    <p>Atenciosamente,<br>Sua Equipe</p>
                </body>
            </html>";

        
        _service.SendEmail(newUser.Email, "Confirmação de Cadastro", emailBody);

        _context.Users?.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

}