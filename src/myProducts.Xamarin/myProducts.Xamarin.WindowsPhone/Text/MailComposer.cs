using System;
using Windows.System;
using Microsoft.Phone.Tasks;
using myProducts.Xamarin.Contracts.Text;

namespace myProducts.Xamarin.WindowsPhone.Text
{
	public class MailComposer : IMailComposer
	{
		public void Compose(string to, string subject, string body)
		{
			var mailTask = new EmailComposeTask();

			mailTask.To = to;
			mailTask.Subject = subject;
			mailTask.Body = body;

			mailTask.Show();
		}
	}
}