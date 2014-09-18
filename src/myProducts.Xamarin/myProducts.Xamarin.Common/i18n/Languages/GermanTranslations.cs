using myProducts.Xamarin.Contracts.i18n;

namespace myProducts.Xamarin.Common.i18n.Languages
{
	public class GermanTranslations : ITranslation
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

		public string LogOut
		{
			get { return "Abmelden"; }
		}

		public string InfoHeadline
		{
			get { return "Info"; }
		}

		public string InfoText
		{
			get
			{
				return
					"Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
					"Curabitur tincidunt dui vitae dolor congue, eget rhoncus odio auctor. " +
					"Class aptent taciti sociosqu ad litora torquent per conubia nostra, per " +
					"inceptos himenaeos. Nam condimentum, metus sit amet accumsan interdum, dui elit " +
					"eleifend mauris, eget molestie ipsum sapien nec felis. Nullam posuere tristique " +
					"ipsum, at malesuada turpis feugiat nec. Quisque vitae lacus vitae nisi malesuada " +
					"semper. In porta, velit nec porta vulputate, leo odio fringilla nisl, quis " +
					"iaculis magna ex ut velit. Suspendisse vel ligula at lorem vehicula fermentum. " +
					"Mauris eget mauris sed felis tempor vestibulum. Integer aliquet leo quis tempus " +
					"pulvinar. Suspendisse ullamcorper turpis vel sem ullamcorper, porttitor pulvinar " +
					"nisi tincidunt. Aenean sed sapien sed dui tristique iaculis vitae sed elit. " +
					"Nullam finibus velit turpis, a dignissim dui rutrum vel. Quisque ultrices, " +
					"diam at condimentum lobortis, urna tortor porttitor dolor, sed bibendum enim " +
					"sem vitae justo. Etiam malesuada pellentesque tortor, in semper magna.";
			}
		}
	}
}