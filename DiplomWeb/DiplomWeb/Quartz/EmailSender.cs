using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Twilio;

namespace DiplomWeb.Quartz
{
    public class EmailSender : IJob
    {
        public void Execute(IJobExecutionContext context)
        {


            //MailMessage msg = new MailMessage();
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //try
            //{
            //    msg.Subject = "Add Subject";
            //    msg.Body = "Add Email Body Part";
            //    msg.From = new MailAddress("pojasotiona@gmail.com");
            //    msg.To.Add("orion10110@yandex.com");
            //    msg.IsBodyHtml = true;
            //    client.Host = "smtp.gmail.com";
            //    System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("pojasoriona@gmail.com", "Borderlands10110");
            //    client.Port = int.Parse("587");
            //    client.EnableSsl = true;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = basicauthenticationinfo;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.Send(msg);
            //}
            //catch(Exception ex)
            //{

            //}

            //using (MailMessage message = new MailMessage("pojasoriona@gmail.com", "orion10110@yandex.ru"))
            //{
            //    message.Subject = "Новостная рассылка";
            //    message.Body = "Новости сайта: бла бла бла";
            //    using (SmtpClient client = new SmtpClient
            //    {
            //        EnableSsl = true,
            //        Host = "smtp.gmail.com",
            //        Port = 465,
            //        Credentials = new NetworkCredential("pojasoriona@gmail.com", "Borderlands10110")
            //    })
            //    {
            //        client.Send(message);
            //    }
            //}
            //string accountSid = "AC5ee2ad646525ef76d596811c23b3dc61";

            //string authToken = "9a339aa9f85fa80c29d9e21a890ea767";

            //string twilioPhoneNumber = "+12566175705";

            //var twilio = new TwilioRestClient(accountSid, authToken);
            //var message = twilio.SendMessage(
            //    twilioPhoneNumber, // From (Replace with your Twilio number)
            //    "+375297872759", // To (Replace with your phone number)
            //    "Hello from C#"
            //    );

            //if (message.RestException != null)
            //{
            //    var error = message.RestException.Message;
            //    Console.WriteLine(error);
            //    Console.Write("Press any key to continue.");
            //    Console.ReadKey();
            //}
        }
    }
}