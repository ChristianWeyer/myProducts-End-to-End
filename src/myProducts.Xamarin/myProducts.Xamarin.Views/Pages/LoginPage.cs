using myProducts.Xamarin.Contracts.Locale;
using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class LoginPage : ContentPage
	{
		private readonly ITranslation _translation;

		public LoginPage(ITranslation translation)
		{
			_translation = translation;
			CreateUI();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			this.SetDefaultPadding();

			Content = new StackLayout()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,

				Children =
				{
					new Label()
					{
						Text = _translation.UserLogin,
						Font = Font.SystemFontOfSize(NamedSize.Large),
					},
					new Entry()
					{
						Placeholder = _translation.UserName,
					},
					new Entry()
					{
						Placeholder = _translation.Password,
						IsPassword = true,
					},
					new Button()
					{
						Text = _translation.LogIn,
					},
					new Footer(),
				}
			};
		}
	}
}