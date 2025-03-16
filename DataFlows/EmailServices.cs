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
            "ahmedshawky985@gmail.com",
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

        public static async Task SendCustomerEmail(string subject, string body, string CustomerEmail)
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
                        mailMessage.To.Add(CustomerEmail);

                        await client.SendMailAsync(mailMessage);
                        Console.WriteLine($"✅ Email successfully sent to Customer {CustomerEmail} recipients.");
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
            string addonsDetails = string.Empty;
            if (bookingDataOut.Addons != null && bookingDataOut.Addons.Count > 0)
            {
                foreach (var addon in bookingDataOut.Addons)
                {
                    addonsDetails += $@"
                        <p><strong>Addon ID:</strong> {addon.AddonID}</p>
                        <p><strong>Addon Name:</strong> {addon.AddonName}</p>
                        <p><strong>Addon Group:</strong> {addon.Addongroup ?? "N/A"}</p>
                        <p><strong>Description:</strong> {addon.Description ?? "N/A"}</p>
                        <p><strong>Unit or Meter Type:</strong> {addon.Unitormeter}</p>
                        <p><strong>Quantity:</strong> {addon.Quantity}</p>
                        <p><strong>Price:</strong> ${addon.Price}</p>
                        <hr>
                    ";
                }
            }
            else
            {
                addonsDetails = "<p>No addons available.</p>";
            }
            return $@"
                    <h2>Booking Confirmed!</h2>
                    <p><strong>Booking ID:</strong> {bookingDataOut.BookingID}</p>
                    <p><strong>Order Type:</strong> {bookingDataOut.ApartmentDTO.ApartmentType}</p>
                    <p><strong>Unit type name:</strong> {bookingDataOut.ApartmentDTO.UnittypeName}</p>
                    <p><strong>Apartment Space:</strong> {bookingDataOut.ApartmentDTO.ApartmentSpace}</p>
                    <p><strong>DeveloperID:</strong> {bookingDataOut.DeveloperID}</p>
                    <p><strong>Customer Name:</strong> {bookingDataOut.CustomerInfo?.Firstname ?? "N/A"}</p>
                    <p><strong>Customer Email:</strong> {bookingDataOut.CustomerInfo?.Email ?? "N/A"} </p>
                    <p><strong>Customer Phone number:</strong> {bookingDataOut.CustomerInfo?.Phonenumber ?? "N/A"} </p>
                    <p><strong>Plan Name:</strong> {bookingDataOut.PlanName}</p>
                    <p><strong>Addons Details:</strong> {addonsDetails}</p>
                    <p><strong>Payment Plan Name:</strong> {bookingDataOut.paymentDTO.Paymentplanname}</p>
                    <p><strong>Number of Installment months:</strong> {bookingDataOut.paymentDTO.Numberofinstallmentmonths}</p>
                    <p><strong>Total Amount:</strong> ${bookingDataOut.TotalAmount}</p>
                    <hr>
                    <p>Thank you for your booking!</p>
                ";
        }

        public static string GenerateCustomerEmailBody(BookingDataOut bookingDataOut)
        {
            string addonsDetails = string.Empty;
            if (bookingDataOut.Addons != null && bookingDataOut.Addons.Count > 0)
            {
                foreach (var addon in bookingDataOut.Addons)
                {
                    addonsDetails += $@"
                        <p><strong>Addon ID:</strong> {addon.AddonID}</p>
                        <p><strong>Addon Name:</strong> {addon.AddonName}</p>
                        <p><strong>Addon Group:</strong> {addon.Addongroup ?? "N/A"}</p>
                        <p><strong>Description:</strong> {addon.Description ?? "N/A"}</p>
                        <p><strong>Unit or Meter Type:</strong> {addon.Unitormeter}</p>
                        <p><strong>Quantity:</strong> {addon.Quantity}</p>
                        <p><strong>Price:</strong> ${addon.Price}</p>
                        <hr>
                    ";
                }
            }
            else
            {
                addonsDetails = "<p>No addons available.</p>";
            }

            return $@"
                    <h2>Dear Customer Booking Confirmed!</h2>
                    <p><strong>Booking ID:</strong> {bookingDataOut.BookingID}</p>
                    <p><strong>Order Type:</strong> {bookingDataOut.ApartmentDTO.ApartmentType}</p>
                    <p><strong>Unit type name:</strong> {bookingDataOut.ApartmentDTO.UnittypeName}</p>
                    <p><strong>Apartment Space:</strong> {bookingDataOut.ApartmentDTO.ApartmentSpace}</p>
                    <p><strong>DeveloperId:</strong> {bookingDataOut.DeveloperID}</p>
                    <p><strong>Plan Name:</strong> {bookingDataOut.PlanName}</p>
                    <p><strong>addonsDetails:</strong> {addonsDetails}</p>
                    <p><strong>Payment Plan:</strong> {bookingDataOut.paymentDTO.Paymentplanname}</p>
                    <p><strong>Number of installment months:</strong> {bookingDataOut.paymentDTO.Numberofinstallmentmonths}</p>
                    <p><strong>Total Amount:</strong> ${bookingDataOut.TotalAmount}</p>
                    <p><strong>Oda Contact Email:</strong> Info@oda-me.com </p>
                    <hr>
                    <p>Thank you for your booking!</p> :
                ";
        }
    }
}
