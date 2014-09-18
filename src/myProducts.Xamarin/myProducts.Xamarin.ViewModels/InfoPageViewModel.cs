using System;
using myProducts.Xamarin.Contracts.Text;
using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.ViewModels
{
	public class InfoPageViewModel : BindableBase, IInfoPageViewModel
	{
		private const string MailSubject = "myProducts Xamarin App";
		private readonly IMailComposer _mailComposer;

		public InfoPageViewModel(IMailComposer mailComposer)
		{
			_mailComposer = mailComposer;
		}

		public void SendInfoMail(string to)
		{
			_mailComposer.Compose(to, MailSubject, String.Empty);
		}
	}
}