using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string email { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
    public string pass { get; set; }
}

