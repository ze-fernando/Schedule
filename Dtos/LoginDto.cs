using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string? email { get; set; }

    [Required]
    public string? pass { get; set; }
}

