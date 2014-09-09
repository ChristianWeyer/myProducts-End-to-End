using myProducts.Xamarin.Contracts.Locale;

namespace myProducts.Xamarin.Common.Locale.Languages
{
	public class GermanTranslation : ITranslation
	{
		public string IsoCode
		{
			get { return "de"; }
		}

		public bool IsDefault
		{
			get { return false; }
		}

		public string UserLogin
		{
			get { return "Anmeldung"; }
		}

		public string UserName
		{
			get { return "Benutzername"; }
		}

		public string Password
		{
			get { return "Passwort"; }
		}

		public string LogIn
		{
			get { return "Anmelden"; }
		}
	}
}