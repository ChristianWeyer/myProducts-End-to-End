using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class MainPage : ContentPage
	{
// ReSharper disable InconsistentNaming
		protected void CreateUI()
// ReSharper restore InconsistentNaming
		{
			Content = new Label()
			{
				Text = "Hello World!"
			};
		}
	}
}