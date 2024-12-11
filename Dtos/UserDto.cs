using System.ComponentModel.DataAnnotations;

namespace Schedule.Dtos;

public class UserDto
{

    [Required(ErrorMessage = "Preencha todos os campos")]
    public string? firstName { get; set; }

    [Required(ErrorMessage = "Preencha todos os campos")]
    public string? lastName { get; set; }

    [Required(ErrorMessage = "Preencha todos os campos")]
    [EmailAddress(ErrorMessage = "Formato de email inváldo")]
    public string? email { get; set; }

    [Required(ErrorMessage = "Preencha todos os campos")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
    public string? password { get; set; }
};
