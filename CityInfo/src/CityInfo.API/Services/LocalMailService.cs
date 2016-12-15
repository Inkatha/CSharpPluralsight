using System;
using System.Diagnostics;

namespace CityInfo.API.Services
{
	public class LocalMailService : IMailService
	{
		public string _mailTo = Startup.Configuration["mailSettings:mailtoAddress"];
		public string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

		public void Send(string subject, string message)
		{
			// send mail - output to debug window
			Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
			Debug.WriteLine($"Subject: {subject}");
			Debug.WriteLine($"Message: {message}");
		}
	}
}
