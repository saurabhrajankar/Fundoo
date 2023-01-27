using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQ
    {
        MessageQueue MQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            MQ.Path = @".\private$\FundooNotes";
            if(!MessageQueue.Exists(MQ.Path))
            { 
               MessageQueue.Create(MQ.Path);
            }
            MQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            MQ.ReceiveCompleted += MQ_ReceiveCompleted;
            MQ.Send(token);
            MQ.BeginReceive();
            MQ.Close();
        }
        private void MQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = MQ.EndReceive(e.AsyncResult);
            string token=message.Body.ToString();
            string subject = "Hello";
            string body = token;
            var smtp=new SmtpClient ("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sauraj600@gmail.com","kexjgwqifwbqevje"),
                EnableSsl=true,
            };
            smtp.Send("sauraj600@gmail.com","sauraj600@gmail.com", subject, body);
            MQ.BeginReceive();
        }

    }
}
