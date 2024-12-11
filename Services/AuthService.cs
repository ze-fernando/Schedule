using Schedule.Models;
using Schedule.Entities;
using Schedule.Dtos;

namespace Schedule.Services;

public class AuthService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public string? Login(string email, string? pass)
    {

        User? user = _context.Users?.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return null;
        else if (!BCrypt.Net.BCrypt.Verify(pass, user.Password))
            return null;

        else
        {
            var token = TokenService.Generate(email);

            return token;
        }

    }

    public User Signin(UserDto user)
    {
        var newUser = new User
        {
            FirstName = user.firstName,
            LastName = user.lastName,
            Email = user.email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.password, 12)
        };

        _context.Users?.Add(newUser);

        _context.SaveChanges();

        return newUser;
    }

}
