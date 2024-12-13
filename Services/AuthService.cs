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
        
        _service.SendEmail(newUser.Email, "Confirmação de Cadastro", 
            $"Clique no link para confirmar seu cadastro: {confirmationLink}");

        _context.Users?.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

}