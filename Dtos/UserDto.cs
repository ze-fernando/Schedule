using System.ComponentModel.DataAnnotations;

namespace Schedule.Dtos;

public class UserDto
{

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Formato de email inváldo")]
    public required string Email { get; set; }

    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
    public required string Password { get; set; }
};
