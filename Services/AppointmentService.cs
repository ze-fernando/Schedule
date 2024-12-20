using System.Globalization;
using System.Security.Claims;
using Schedule.Dtos;
using Schedule.Services;
using Schedule.Entities;
using Schedule.Models;

namespace Schedule.Services;

public class AppointmentService(AppDbContext context, EmailService s)
{
    private readonly AppDbContext _context = context;
    private readonly EmailService _service = s;

    public Appointment CreateAppointment(AppointmentDto ap, string id)
    {
        
        var _appointment = new Appointment{
            Date =  DateTime.ParseExact(ap.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Hour =  DateTime.ParseExact(ap.Hour, "HH:mm", CultureInfo.InvariantCulture),
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

    public Appointment? ReadOnlyAppointment(string userId, int id)
    {
        var appointment = _context.Schedules
        .FirstOrDefault(x => x.UserId == int.Parse(userId) && x.Id == id);

        return appointment;        
    }

    public Appointment? UpdateAppointment(AppointmentDto ap, string userId, int id)
    {
        var appointment = _context.Schedules.FirstOrDefault(x =>
        x.UserId == int.Parse(userId) && x.Id == id);

        if(appointment == null)
            return null;c
        
        appointment.Date =  DateTime.ParseExact(ap.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        appointment.Hour =  DateTime.ParseExact(ap.Date, "HH:mm", CultureInfo.InvariantCulture);
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