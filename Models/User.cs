using System.Text.Json.Serialization;
using Schedule.Models;

namespace Schedule.Models;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool IsConfirmed { get; set; } = false;
    public required string Token { get; set; }
    
    [JsonIgnore]
    public ICollection<Appointment>? Schedules { get; set; }
}
