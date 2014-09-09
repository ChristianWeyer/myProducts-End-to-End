using myProducts.Xamarin.Contracts.Locale;

namespace myProducts.Xamarin.Common.Locale.Languages
{
	public class EnglishTranslation : ITranslation
	{
		public string IsoCode
		{
			get { return "en"; }
		}

		public bool IsDefault
		{
			get { return true; }
		}

		public string UserLogin
		{
			get { return "User Login"; }
		}

		public string UserName
		{
			get { return "User name"; }
		}

		public string Password
		{
			get { return "Password"; }
		}

		public string LogIn
		{
			get { return "Log in"; }
		}
	}
}