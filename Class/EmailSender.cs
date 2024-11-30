using System;
using System.Net;
using System.Net.Mail;

public class EmailSender
{
    private const string SmtpServer = "smtp.gmail.com"; // Your SMTP server
    private const int SmtpPort = 587; // SMTP port for Gmail
    private const string SenderEmail = "campuscloset4@gmail.com"; // Your email address
    private const string SenderPassword = "vjuo epie fnqv vceu"; // Your email password
    public static Boolean emailsent = false;

    public static void SendVerificationEmail(string recipientEmail, int otpCode)
    {
        try
        {
            // Create a new MailMessage
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(SenderEmail),
                Subject = "OTP Code",
                Body = $"Dear Honorable Client,\n\n" +  // Two newlines for a blank line
                       $"I hope this message finds you in great health.\n" +
                       $"Your OTP code is: {otpCode}\n\n" +  // Another blank line
                       $"Warm Regards,\n" +
                       $"Sales Team\n" +
                       $"Campus Closet",
                IsBodyHtml = false
            };
            mail.To.Add(recipientEmail);

            // Set up the SMTP client
            SmtpClient smtpClient = new SmtpClient(SmtpServer, SmtpPort)
            {
                Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                EnableSsl = true // Use SSL for secure email
            };

            // Send the email
            smtpClient.Send(mail);
            EmailSender.emailsent = true;
        }
        catch (Exception ex)
        {
            EmailSender.emailsent = false;
        }
    }
}