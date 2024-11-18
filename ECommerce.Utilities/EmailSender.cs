//using Microsoft.AspNetCore.Identity.UI.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ECommerce.Utilities
//{
//    public class EmailSender : IEmailSender
//    {
//        public Task SendEmailAsync(string email, string subject, string htmlMessage)
//            => Task.CompletedTask;
//    }
//}
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailSender : IEmailSender
{
    private readonly string smtpHost = "smtp.gmail.com";
    private readonly int smtpPort = 587; // Typically 587 or 465 for TLS/SSL
    private readonly string smtpUser = "ahmedrefaatsenger@gmail.com";
    private readonly string smtpPass = "Ihf@131996";

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using (var client = new SmtpClient(smtpHost, smtpPort))
        {
            client.Credentials = new NetworkCredential(smtpUser, smtpPass);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUser, "Your Display Name"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            try
            {
                await client.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP error occurred: {smtpEx.StatusCode}, {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error occurred: {ex.Message}");
            }
        }
    }


}
