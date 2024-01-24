using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Service
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailModel _email;
        public EmailServices(EmailModel email)
        {
            _email= email;
        }
        public string sendEmail(Message message)
        {
            var createEmail = CreateEmailMessage(message);
            var sendMessage = sendmail(createEmail);
            if(sendMessage == "success")
            {
                return "ok";
            }
            return "error";
        }

        public MimeMessage CreateEmailMessage(Message message) 
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress("email",_email.From));
            email.To.AddRange(message.To);
            email.Subject = message.Subject;
            email.Body = new TextPart("html")
            {
                Text = message.Content
            };
            return email;
        }

        public string sendmail(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_email.SmtpServer, _email.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_email.userName, _email.Password);
                client.Send(message);
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally 
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
