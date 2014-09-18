using Android.Content;
using Android.Net;
using myProducts.Xamarin.Contracts.Text;
using Xamarin.Forms;

namespace myProducts.Xamarin.Android.Text
{
	public class MailComposer : IMailComposer
	{
		public void Compose(string to, string subject, string body)
		{
			var mailIntent = new Intent(Intent.ActionSend);
			mailIntent.PutExtra(Intent.ExtraEmail, to);
			mailIntent.PutExtra(Intent.ExtraSubject, subject);
			mailIntent.PutExtra(Intent.ExtraText, body);
			Forms.Context.StartActivity(mailIntent);
		}
	}
}