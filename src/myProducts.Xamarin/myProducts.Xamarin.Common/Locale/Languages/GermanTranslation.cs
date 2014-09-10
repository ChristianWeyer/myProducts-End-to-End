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

		public string LogInNotPossible
		{
			get { return "Anmeldung fehlgeschlagen."; }
		}

		public string Articles
		{
			get { return "Artikel"; }
		}

		public string Gallery
		{
			get { return "Galerie"; }
		}

		public string Logs
		{
			get { return "Logs"; }
		}

		public string Statistics
		{
			get { return "Statistiken"; }
		}

		public string Info
		{
			get { return "Info"; }
		}

		public string Overview
		{
			get { return "Überblick"; }
		}

		public string Search
		{
			get { return "Suchen"; }
		}

		public string Name
		{
			get { return "Name"; }
		}

		public string Code
		{
			get { return "Code"; }
		}

		public string Description
		{
			get { return "Beschreibung"; }
		}
	}
}