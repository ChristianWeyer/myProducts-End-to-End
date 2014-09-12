using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Components
{
	public class Footer : ContentView
	{
		public Footer()
		{
			CreateUI();
		}

		private void CreateUI()
		{
			VerticalOptions = LayoutOptions.End;
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