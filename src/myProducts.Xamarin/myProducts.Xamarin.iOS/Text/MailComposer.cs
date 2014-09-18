using MonoTouch.MessageUI;
using MonoTouch.UIKit;
using myProducts.Xamarin.Contracts.Text;
using Xamarin.Forms;

namespace myProducts.Xamarin.iOS.Text
{
	public class MailComposer : IMailComposer
	{
		public void Compose(string to, string subject, string body)
		{
			var mail = new MFMailComposeViewController();
			mail.SetToRecipients(new [] {to});
			mail.SetSubject(subject);
			mail.SetMessageBody(body, false);

			mail.Finished += (sender, args) => args.Controller.DismissViewController(true, null);

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mail, true, null);
		}
	}
}