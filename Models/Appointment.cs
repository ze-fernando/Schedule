using System.Text.Json.Serialization;

namespace Schedule.Models;

public class Appointment
{
    public int Id { get; set; }
    public required DateTime Date { get; set; }
    public required DateTime Hour { get; set; }
    public required string Place { get; set; }
    public required string Task { get; set; }
    public required Priority TaskPriority { get; set; }
    
    public int UserId { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; }



    public override string ToString()
    {
        return $@"
        <div style='border: 1px solid #ddd; padding: 10px; margin: 10px;'>
            <h3 style='color: #2c3e50;'>{Task}</h3>
            <p><strong>Data:</strong> {Date:dd/MM/yyyy}</p>
            <p><strong>Hora:</strong> {Hour:HH:mm}</p>
            <p><strong>Local:</strong> {Place}</p>
            <p><strong>Prioridade:</strong> {TaskPriority}</p>
        </div>";
    }
}
