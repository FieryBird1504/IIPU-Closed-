using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace IIPU_lab8
{
	class Emailer
	{
		private readonly SmtpClient SmtpClient;
		private const string Sender = "fakeyfoxe@gmail.com";
		private string Password = "FoxesAreFake";

		private const string Host = "smtp.gmail.com";
		private const int Port = 587;

		public Emailer()
		{
			SmtpClient = new SmtpClient(Host, Port)
			{
				Credentials = new System.Net.NetworkCredential(Sender, Password),
				EnableSsl = true
			};
		}

		public void SendEmail(string receiver, string topic, string filePath)
		{
			var mail = new MailMessage(Sender, receiver, topic, string.Empty);
			using(var attachment = new Attachment(filePath))
			{
				mail.Attachments.Add(attachment);
				try
				{
					SmtpClient.Send(mail);
				}
				catch(Exception) { }
			}
		}
	}
}
