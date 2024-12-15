using System.ComponentModel.DataAnnotations;
using Schedule.Models;

namespace Schedule.Dtos;

public class AppointmentDto
{
    public required string Place { get; set; }

    public required string Task { get; set; }
    
    public required Priority TaskPriority { get; set; }

    public required string Date { get; set; }

    public required string Hour { get; set; }
}