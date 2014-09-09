using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class MainPage : ContentPage
	{
		public MainPage()
		{
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
						Text = "User login",
						Font = Font.SystemFontOfSize(NamedSize.Large),
					},
					new Entry()
					{
						Placeholder = "Username",
					},
					new Entry()
					{
						Placeholder = "Password",
						IsPassword = true,
					},
					new Button()
					{
						Text = "Log in",
					},
					new Footer(),
				}
			};
		}
	}
}