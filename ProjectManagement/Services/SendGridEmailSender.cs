using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using ProjectManagement.Models;

public class SendGridEmailSender : IEmailSender
{
    private readonly SendGridClient _client;
    private readonly string _senderEmail;

    public SendGridEmailSender(IOptions<SendGridOptions> optionsAccessor)
    {
        var options = optionsAccessor.Value ?? throw new ArgumentNullException(nameof(optionsAccessor.Value));
        _client = new SendGridClient(options.ApiKey ?? throw new ArgumentNullException(nameof(options.ApiKey)));
        _senderEmail = options.SenderEmail ?? throw new ArgumentNullException(nameof(options.SenderEmail));
    }

    public async System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var msg = new SendGridMessage
        {
            From = new EmailAddress(_senderEmail, "Your Sender Name"),
            Subject = subject,
            HtmlContent = htmlMessage
        };
        msg.AddTo(new EmailAddress(email));

        var response = await _client.SendEmailAsync(msg);
    }
}
