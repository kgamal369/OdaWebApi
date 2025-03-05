using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using OdaWepApi.Domain.DTOs;
namespace OdaWepApi.DataFlows
{
    public static class EmailService
    {
        private static readonly string smtpServer = "smtp.gmail.com";
        private static readonly int smtpPort = 587; // Gmail SMTP Port
        private static readonly string smtpUsername = "OdaWebApiBookingEmail@gmail.com";
        private static readonly string smtpPassword = "ebtn rtlo xcih frcp";

        public static readonly List<string> recipientEmails = new List<string>
        {
            "eng.karim.gamal369@gmail.com",
            "ashawkyh@gmail.com",
            "mohamed.k.elhawary@gmail.com",
            "Info@oda-me.com"
        };

        public static async Task SendEmailToAllRecipients(string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true; // Ensure SSL is enabled
                    client.UseDefaultCredentials = false; // Ensure explicit credentials are used


                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(smtpUsername);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        // Add all recipients
                        foreach (var recipient in recipientEmails)
                        {
                            mailMessage.To.Add(recipient);
                        }

                        await client.SendMailAsync(mailMessage);
                        Console.WriteLine($"✅ Email successfully sent to {recipientEmails.Count} recipients.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Email sending failed: {ex.Message}");
            }
        }

        public static string GenerateEmailBody(BookingDataOut bookingDataOut)
        {
            return $@"
                    <h2>Booking Confirmed!</h2>
                    <p><strong>Booking ID:</strong> {bookingDataOut.BookingID}</p>
                    <p><strong>Order Type:</strong> {bookingDataOut.ApartmentType}</p>
                    <p><strong>Unit type name:</strong> {bookingDataOut.UnittypeName}</p>
                    <p><strong>Apartment Space:</strong> {bookingDataOut.ApartmentSpace}</p>
                    <p><strong>Customer:</strong> {bookingDataOut.CustomerInfo?.Firstname ?? "N/A"}</p>
                    <p><strong>Customer Email:</strong> {bookingDataOut.CustomerInfo?.Email ?? "N/A"} </p>
                    <p><strong>Customer Phone number:</strong> {bookingDataOut.CustomerInfo?.Phonenumber ?? "N/A"} </p>
                    <p><strong>Payment Plan:</strong> {bookingDataOut.PlanName}</p>
                    <p><strong>Payment Plan:</strong> {bookingDataOut.paymentDTO.Paymentplanname}</p>
                    <p><strong>Total Amount:</strong> ${bookingDataOut.TotalAmount}</p>
                    <p><strong>Status:</strong> Confirmed</p>
                    <hr>
                    <p>Thank you for your booking!</p>
                ";
        }
    }
}
