using System.Net;
using System.Net.Mail;
using DotNetEnv;
using Schedule.Entities;
using Schedule.Models;

namespace Schedule.Services;

public class EmailService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async void SendEmail(string to, string subject, string body)
    {
        
        string senderMail = Settings.ServerEmail;
        string senderPass = Settings.ServerPassword;
        
        using var smtpClient = new SmtpClient(Settings.HostSmtp, Settings.PortSmtp)
        {
            Credentials = new NetworkCredential(senderMail, senderPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(senderMail, to, subject, body)
        {
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task<bool> ConfirmEmail(string token)
    {
        var user = _context.Users.FirstOrDefault(x => x.Token == token);

        if(user == null)
            return false;
        
        user.IsConfirmed = true;
        user.Token = "null";

        await _context.SaveChangesAsync();

        return true;
    }

    public void SendSchedule(string userId)
    {
        DateTime today = DateTime.Today;

        User user = _context.Users.First(x => x.Id == int.Parse(userId));

        if(user.IsConfirmed)
        {
            ICollection<Appointment> appointments = user.Schedules.Where(x => x.Date.Date == today).ToList();
            foreach(var ap in appointments)
            {
                SendEmail(user.Email, "Tarefa do dia", $"Olá {user.FirstName} você tem uma tarefa para hoje {ap}");
            }
        }
    }

}