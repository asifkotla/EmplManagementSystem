using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using EmplManagementSystem.Utility1;
using System.Linq.Expressions;
using EmplManagementSystem.HR_Dashboard;

namespace EmplManagementSystem.SMTP
{
    internal class SendMail
    {
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string Body { get; set; }


        string fromemail = "employeeemanagementsystem@gmail.com";
        string frompass = "hnnq loam hhja romh";

        public void SendEmail(SendMail sendMail)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromemail);
                message.Subject = sendMail.Subject;
                message.To.Add(new MailAddress(sendMail.ToEmail));
                message.Body = sendMail.Body;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromemail, frompass),
                    EnableSsl = true
                };
                smtpClient.Send(message);
                Utility.DisplaySuccessMessage("Mail Sent");
                HR hr = new HR();
                hr.HandleMainmenu(null);

            }
            catch (Exception ex) 
                {
                    Console.WriteLine("Something Went Wrong"+ex.Message);
                    HR hr = new HR();
                    hr.HandleMainmenu(null);
                }
      
        }

    }
}
