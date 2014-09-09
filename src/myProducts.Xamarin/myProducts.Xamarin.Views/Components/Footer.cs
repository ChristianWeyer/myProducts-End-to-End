using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Components
{
	public class Footer : ContentView
	{
		public Footer()
		{
			CreateUI();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			VerticalOptions = LayoutOptions.EndAndExpand;
			HorizontalOptions = LayoutOptions.FillAndExpand;

			Content = new Label()
			{
				// TODO: Replace with image
				Text = "thinktecture",
				HorizontalOptions = LayoutOptions.Center,
			};
		}
	}
}