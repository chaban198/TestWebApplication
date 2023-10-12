using System.Net;
using System.Net.Mail;
using GlobalDomain.Models.Exceptions;
using IdentityServer.Models.Options;
using Microsoft.Extensions.Options;

namespace Services;

public class EMailService : IEMailService
{
    private const string SenderName = "TaskListWebApi";
    private const string DefaultMessageHeader = "TaskListWebApi Message (noreply)";

    private readonly SmtpOptions _smtpOptions;

    public EMailService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions.Value;
    }

    public async Task SendMail(string destination, string message, string? messageHeader = null)
    {
        if (_smtpOptions.EMail is null)
            throw new ConfigurationException(typeof(SmtpOptions), "Email not exist");
        
        var from = new MailAddress(_smtpOptions.EMail, SenderName);
        var to = new MailAddress(destination);
        using var mail = new MailMessage(from, to);

        mail.Subject = messageHeader ?? DefaultMessageHeader;
        mail.Body = message;

        using var client = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port);
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(_smtpOptions.EMail, _smtpOptions.Password);

        await client.SendMailAsync(mail);
    }
}