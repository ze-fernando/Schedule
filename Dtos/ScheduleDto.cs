using System.ComponentModel.DataAnnotations;


public class ScheduleDto
{
    [Required]
    private string? Place { get; set; }
    [Required]
    private string? Task { get; set; }
    [Required]
    private Priority TaskPriority { get; set; }
    [Required]
    private DateTime Date { get; set; }
    [Required]
    private User? TaskUser { get; set; }
}

