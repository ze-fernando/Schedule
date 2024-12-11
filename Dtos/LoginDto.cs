using System.ComponentModel.DataAnnotations;

namespace Schedule.Dtos;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string? email { get; set; }

    [Required]
    public string? password { get; set; }
}

