using System.Net;
using System.Net.Mail;
using System.Text;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
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

    public void SendSchedule()
    {
        ICollection<User> users = _context.Users
        .Include(u => u.Schedules)
        .Where(x => x.IsConfirmed)
        .ToList();

        DateTime today = DateTime.Today;

        foreach(var user in users)
        {
            StringBuilder emailBody = new StringBuilder();
            
            emailBody.Append($"<h1>Olá {user.FirstName}</h1>");
            emailBody.Append("<h2>Suas tarefas para hoje:</h2>");
            
            ICollection<Appointment> appointments = user.Schedules
            .Where(x => x.Date.Date == today)
            .ToList() ?? new List<Appointment>();;
            
            if (appointments == null)
            {
                emailBody.Append("<p>Você não tem tarefas agendadas para hoje.</p>");
            }
            else
            {
                foreach (var ap in appointments)
                {
                    emailBody.Append(ap.ToString());
                }
            }

            SendEmail(user.Email, "Tarefas do Dia", emailBody.ToString());
        }
    }

}