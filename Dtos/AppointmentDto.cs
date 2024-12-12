using System.ComponentModel.DataAnnotations;
using Schedule.Models;

namespace Schedule.Dtos;

public class AppointmentDto
{
    [Required]
    public string? Place { get; set; }

    [Required]
    public string? Task { get; set; }
    
    public Priority TaskPriority { get; set; }

    [Required]
    public string? Date { get; set; }
}