namespace Schedule.Models;

public class Appointment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
    public string? Task { get; set; }
    public Priority TaskPriority { get; set; }
    
    public int UserId { get; set; }
}
