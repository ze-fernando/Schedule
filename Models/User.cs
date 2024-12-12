using System.Text.Json.Serialization;
using Schedule.Models;

namespace Schedule.Models;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    [JsonIgnore]
    public ICollection<Appointment>? Schedules { get; set; }
}
