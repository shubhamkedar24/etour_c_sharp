using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace indiatour_webapi_master.Models
{
    public class EmailTo
    {

        public static void sendmail(string customermail, booking booking, string packageName)
        {
            string fromMail = "samplegroup05@gmail.com";
            string fromPassword = "foqclihfdhdguvmt";

            int bookingId = booking.Booking_Id;
            DateTime bookingDate = booking.Bookingdate;
            double tourCost = booking.Totalamount;


            string invoice = "<html>" +
                             "<body>" +
                             "<div>" +
                             "<h3>"+
                             "These are your booking details " +
                             "</h3>" +
                             "</div>" +
                             "<table\">" +
                               "<tbody>" +
                                  "<tr>" +
                                  "<td><b>Booking id</b></td>" +
                                  "<td> : </td>" +
                                  "<td>" + bookingId + "</td>" +
                                  "</tr>" +
                                  "<tr>" +
                                  "<td><b>Package name</b></td>" +
                                  "<td> : </td>" +
                                  "<td>" + packageName + "</td>" +
                                  "</tr>" +
                                  "<tr>" +
                                  "<td><b>Booking date</b></td>" +
                                  "<td> : </td>" +
                                  "<td>" + bookingDate + "</td>" +
                                  "</tr>" +
                                  "<tr>" +
                                  "<td><b>Tour Cost</b></td>" +
                                  "<td> : </td>" +
                                  "<td>" + "Rs. " + tourCost + "/-" + "</td>" +
                                  "</tr>" +
                                  "<h3>" +
                                  "Thank you.... " +
                                  "</h3>" +
                             "</tbody>" +
                             "</table>" +
                             "</body>" +
                             "</html>";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Test Subject";
            message.To.Add(new MailAddress(customermail));
            message.Body = invoice;

            string newnow = DateTime.Now.ToString("yyyy-MM-dd");

            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}