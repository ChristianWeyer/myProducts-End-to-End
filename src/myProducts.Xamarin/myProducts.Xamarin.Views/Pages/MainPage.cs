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
			Content = new Label()
			{
				Text = "Hello World!"
			};
		}
	}
}