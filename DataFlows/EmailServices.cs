using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.Threading.Tasks;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Models.Forms;
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

        public static string FormatCurrency(decimal value)
        {
            return value.ToString("#,##0.00", CultureInfo.InvariantCulture);
        }
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
            string addonsDetails = bookingDataOut.Addons != null && bookingDataOut.Addons.Count > 0
       ? string.Join("", bookingDataOut.Addons.Select(addon => $@"
            <tr>
                <td>{addon.AddonName}</td>
                <td>{addon.Addongroup ?? "N/A"}</td>
                <td>{addon.Description ?? "N/A"}</td>
                <td>{addon.Quantity}</td>
                <td>{FormatCurrency(addon.Price)}</td>
            </tr>
        "))
       : "<tr><td colspan='5'>No Add-ons Available.</td></tr>";

            string questionsDetails = bookingDataOut.CustomerAnswers != null && bookingDataOut.CustomerAnswers.Count > 0
                ? string.Join("", bookingDataOut.CustomerAnswers.Select(q => $"<p><strong>{q.Questiontext}:</strong> {q.Answertext}</p>"))
                : "<p>No additional questions provided.</p>";

            return $@"
        <h2>New Booking Notification</h2>
        <p>A new booking has been placed. Please prepare for processing and contact the customer.</p>
        <hr>
        <h3>📌 Booking Details</h3>
        <p><strong>Order ID:</strong> {bookingDataOut.BookingID}</p>
        <p><strong>Customer Name:</strong> {bookingDataOut.CustomerInfo?.Firstname ?? "N/A"}</p>
        <p><strong>Customer Phone:</strong> {bookingDataOut.CustomerInfo?.Phonenumber ?? "N/A"}</p>
        <p><strong>Customer Email:</strong> {bookingDataOut.CustomerInfo?.Email ?? "N/A"}</p>
        <p><strong>Order Type:</strong> {bookingDataOut.ApartmentDTO.ApartmentType}</p>
        
        <h3>🏡 Unit Details</h3>
        <p><strong>Developer:</strong> {bookingDataOut.DeveloperID}</p>
        <p><strong>Unit Type:</strong> {bookingDataOut.ApartmentDTO.UnittypeName}</p>
        <p><strong>Unit Area:</strong> {bookingDataOut.ApartmentDTO.ApartmentSpace} m²</p>

        <h3>📋 Plan Details</h3>
        <p><strong>Selected Plan:</strong> {bookingDataOut.PlanName}</p>

        <h3>🛠 Add-ons Details</h3>
        <table border='1' cellspacing='0' cellpadding='5'>
            <tr>
                <th>Addon Name</th>
                <th>Group</th>
                <th>Description</th>
                <th>Quantity</th>
                <th>Price</th>
            </tr>
            {addonsDetails}
        </table>

        <h3>💳 Payment Plan</h3>
        <p><strong>Payment Program:</strong> {bookingDataOut.paymentDTO.Paymentplanname}</p>
        <p><strong>Installment Duration:</strong> {bookingDataOut.paymentDTO.Numberofinstallmentmonths} Months</p>
        <p><strong>Total Amount:</strong> {FormatCurrency(bookingDataOut.TotalAmount)}</p>

        <h3>❓ Questions & Answers</h3>
        {questionsDetails}

        <hr>
        <p><strong>Please process this order and coordinate with the customer.</strong></p>
        <p>Best Regards,</p>
        <p>The Oda Team</p>
    ";
        }

        public static string GenerateCustomerEmailBody(BookingDataOut bookingDataOut)
        {
            string addonsDetails = bookingDataOut.Addons != null && bookingDataOut.Addons.Count > 0
          ? string.Join("", bookingDataOut.Addons.Select(addon => $@"
            <tr>
                <td>{addon.AddonName}</td>
                <td>{addon.Addongroup ?? "N/A"}</td>
                <td>{addon.Quantity}</td>
                <td>{FormatCurrency(addon.Price)}</td>
            </tr>
        "))
          : "<tr><td colspan='4'>No Add-ons Available.</td></tr>";

            return $@"
        <h2>🎉 Congratulations, {bookingDataOut.CustomerInfo?.Firstname ?? "Valued Customer"}!</h2>
        <p>Your order has been successfully received. Welcome to ODA - we’re thrilled to have you on board!</p>
        <p>Our team will review your order and reach out soon to schedule a meeting with our sales architect.</p>
        <p>If you have any questions, feel free to reply to this email.</p>
        <hr>
        
        <h3>📌 Booking Summary</h3>
        <p><strong>Order ID:</strong> {bookingDataOut.BookingID}</p>
        <p><strong>Order Type:</strong> {bookingDataOut.ApartmentDTO.ApartmentType}</p>

        <h3>🏡 Your Unit Details</h3>
        <p><strong>Developer:</strong> {bookingDataOut.DeveloperID}</p>
        <p><strong>Unit Type:</strong> {bookingDataOut.ApartmentDTO.UnittypeName}</p>
        <p><strong>Unit Area:</strong> {bookingDataOut.ApartmentDTO.ApartmentSpace} m²</p>

        <h3>📋 Plan Details</h3>
        <p><strong>Selected Plan:</strong> {bookingDataOut.PlanName}</p>

        <h3>🛠 Add-ons Details</h3>
        <table border='1' cellspacing='0' cellpadding='5'>
            <tr>
                <th>Addon Name</th>
                <th>Group</th>
                <th>Quantity</th>
                <th>Price</th>
            </tr>
            {addonsDetails}
        </table>

        <h3>💳 Payment Program</h3>
        <p><strong>Payment Plan:</strong> {bookingDataOut.paymentDTO.Paymentplanname}</p>
        <p><strong>Installment Duration:</strong> {bookingDataOut.paymentDTO.Numberofinstallmentmonths} Months</p>
        <p><strong>Total Amount:</strong> {FormatCurrency(bookingDataOut.TotalAmount)}</p>

        <h3>📞 ODA Contact Details</h3>
        <p><strong>Email:</strong> info@oda-me.com</p>
        <p><strong>Phone:</strong> +20 108 055 559 7</p>
        <p><strong>Website:</strong> <a href='https://www.oda-me.com'>www.oda-me.com</a></p>

        <hr>
        <p>Great to connect and best wishes,</p>
        <p><strong>The Oda Team</strong></p>
    ";
        }


        public static string GenerateEmailOdaAmbassadorBody(Odaambassador odaAmbassador)
        {
            return $@"
        <h2>New Ambassador Registration</h2>
        <p>A new ambassador has registered. Please review their details and contact them.</p>
        <hr>
        <h3>📌 Ambassador Details</h3 >
        <p><strong>Owner Name:</strong> {odaAmbassador.Ownername}</p>
        <p><strong>Owner Phone:</strong> {odaAmbassador.Ownerphonenumber}</p>
        <p><strong>Owner Unit Area:</strong> {odaAmbassador.Ownerunitarea} m²</p>
        <p><strong>Owner Unit Location:</strong> {odaAmbassador.Ownerunitlocation}</p>
        <p><strong>Owner Developer:</strong> {odaAmbassador.Ownerdeveloper}</p>
        <p><strong>Owner Select Budget:</strong> {odaAmbassador.Ownerselectbudget}</p>
        <p><strong>Referral Name:</strong> {odaAmbassador.Referralname}</p>
        <p><strong>Referral Phone:</strong> {odaAmbassador.Referralphonenumber}</p>
        <p><strong>Referral Email:</strong> {odaAmbassador.Referralemail}</p>
        <p><strong>Referral Client Statue:</strong> {odaAmbassador.Referralclientstatue}</p>
        <hr>
        <p><strong>Please review this registration and contact the ambassador.</strong></p>
        <p>Best Regards,</p>
        <p>The Oda Team</p>
        ";
        }


        public static string GenerateEmailContactUsBody(Contactus contactus)
        {
            return $@"
            
            <h2>New Contact Us Request</h2>
            <p>A new contact us request has been submitted. Please review the details and contact the customer.</p>
            <hr>
            <h3>📌 Contact Us Details</h3
            <p><strong>Full Name:</strong> {contactus.Firstname}</p>
            <p><strong>Email:</strong> {contactus.Email}</p>
            <p><strong>Phone:</strong> {contactus.Phonenumber}</p>
            <p><strong>Message:</strong> {contactus.Comments}</p>
            <hr>
            <p><strong>Please review this request and contact the customer.</strong></p>
            <p>Best Regards,</p>
            <p>The Oda Team</p>
            ";
        }

    }
}
