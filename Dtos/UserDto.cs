using System.ComponentModel.DataAnnotations;

public class UserDto
{

    [Required(ErrorMessage = "Preencha todos os campos")]
    string? fisrtName { get; set; }

    [Required(ErrorMessage = "Preencha todos os campos")]
    string? lastName { get; set; }

    [Required(ErrorMessage = "Preencha todos os campos")]
    [EmailAddress(ErrorMessage = "Formato de email inváldo")]
    string? email { get; set; }

    [Required(ErrorMessage = "Preencha todos os campos")]
    string? password { get; set; }
};
