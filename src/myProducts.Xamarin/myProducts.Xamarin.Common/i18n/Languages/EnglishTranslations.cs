using myProducts.Xamarin.Contracts.i18n;

namespace myProducts.Xamarin.Common.i18n.Languages
{
	public class EnglishTranslations : ITranslation
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

		public string LogInNotPossible
		{
			get { return "Login failed."; }
		}

		public string Articles
		{
			get { return "Articles"; }
		}

		public string Gallery
		{
			get { return "Gallery"; }
		}

		public string Logs
		{
			get { return "Logs"; }
		}

		public string Statistics
		{
			get { return "Statistics"; }
		}

		public string Info
		{
			get { return "Info"; }
		}

		public string Overview
		{
			get { return "Overview"; }
		}

		public string Search
		{
			get { return "Search"; }
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
			get { return "Description"; }
		}

		public string LogOut
		{
			get { return "Log out"; }
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