using System.ComponentModel.DataAnnotations;


public class ScheduleDto
{
    [Required]
    public string? Place { get; set; }
    [Required]
    public string? Task { get; set; }
    [Required]
    public Priority TaskPriority { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public User? TaskUser { get; set; }
}

