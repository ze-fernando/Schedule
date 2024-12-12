using System.Globalization;
using System.Security.Claims;
using Schedule.Dtos;
using Schedule.Entities;
using Schedule.Models;

namespace Schedule.Services;

public class AppointmentService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public Appointment CreateAppointment(AppointmentDto ap, string id)
    {
        
        var _appointment = new Appointment{
            Date =  DateTime.ParseExact(ap.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Place = ap.Place,
            Task = ap.Task,
            TaskPriority = ap.TaskPriority,
            UserId = int.Parse(id)
        };

        _context.Schedules?.Add(_appointment);
        _context.SaveChanges();

        return _appointment;
    }

    public ICollection<Appointment> ReadAppointment(string id)
    {
        ICollection<Appointment> appointments = _context.Schedules
        .Where(x => x.UserId == int.Parse(id))
        .ToList();

        return appointments;
    }
 
}
