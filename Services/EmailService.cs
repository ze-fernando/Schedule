using System.Net;
using System.Net.Mail;
using DotNetEnv;
using Schedule.Entities;

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
            IsBodyHtml = false
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

}