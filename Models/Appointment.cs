using System.Text.Json.Serialization;

namespace Schedule.Models;

public class Appointment
{
    public int Id { get; set; }
    public required DateTime Date { get; set; }
    public required string Place { get; set; }
    public required string Task { get; set; }
    public required Priority TaskPriority { get; set; }
    
    public int UserId { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; }
}
