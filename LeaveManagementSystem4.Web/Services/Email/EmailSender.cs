namespace LeaveManagementSystem4.Web.Services.Email;


public class EmailSender(IConfiguration _configuration) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
     
       var fromAddress = _configuration["EmailSettings:DefaultEmailAddress"]; // Get the default email address from configuration
        var smtpServe = _configuration["EmailSettings:Server"];
       var smtpPort = Convert.ToInt32(_configuration["EmailSettings:Port"]);
        var message = new MailMessage
        {
            // Set the sender email address
            From = new MailAddress(fromAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        // Add the recipient email address
        message.To.Add(new MailAddress(email));
        // Create an SMTP client to send the email
        using var client = new SmtpClient(smtpServe, smtpPort);
        await client.SendMailAsync(message);

    }
}
