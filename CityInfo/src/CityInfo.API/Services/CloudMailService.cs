using System.Diagnostics;

namespace CityInfo.API.Services
{
	public class CloudMailService : IMailService
	{
		public string _mailTo = "admin@mycompany.com";
		public string _mailFrom = "noreply@mycompany.com";

		public void Send(string subject, string message)
		{
			// send mail - output to debug window
			Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
			Debug.WriteLine($"Subject: {subject}");
			Debug.WriteLine($"Message: {message}");
		}
	}
}
