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

    public ICollection<Appointment> ReadAppointment(string userId)
    {
        ICollection<Appointment> appointments = _context.Schedules
        .Where(x => x.UserId == int.Parse(userId))
        .ToList();

        return appointments;
    }

    public Appointment ReadOnlyAppointment(string userId, int id)
    {
        var appointment = _context.Schedules
        .FirstOrDefault(x => x.UserId == int.Parse(userId) && x.Id == id);

        if(appointment != null)     
            return (Appointment)appointment;
        
        return null;
    }

    public Appointment UpdateAppointment(AppointmentDto ap, string userId, int id)
    {
        var appointment = _context.Schedules.FirstOrDefault(x =>
        x.UserId == int.Parse(userId) && x.Id == id);

        if(appointment == null)
            return null;
        
        appointment.Date =  DateTime.ParseExact(ap.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        appointment.Place = ap.Place;
        appointment.Task = ap.Task;
        appointment.TaskPriority = ap.TaskPriority;

        _context.SaveChanges();
        
        return appointment;
    }

    public bool DeleteAppointment(string userId, int id)
    {
        var appointment = _context.Schedules.FirstOrDefault(x =>
        x.UserId == int.Parse(userId) && x.Id == id);

        if(appointment == null)
            return false;

        _context.Remove(appointment);
        _context.SaveChanges();
        return true;
    }
}