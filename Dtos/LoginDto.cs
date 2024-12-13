using System.ComponentModel.DataAnnotations;

namespace Schedule.Dtos;

public class LoginDto
{
    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }
}

