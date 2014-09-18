using System;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Components
{
	public class Footer : ContentView
	{
		private const string ThinktectureLogoFileName = "thinktecture_logo.png";

		public Footer()
		{
			CreateUI();
		}

		private void CreateUI()
		{
			VerticalOptions = LayoutOptions.End;
			HorizontalOptions = LayoutOptions.FillAndExpand;

			var image = new Image()
			{
				Source = ThinktectureLogoFileName.ToDeviceImage(),
				HorizontalOptions = LayoutOptions.Center
			};

			var label = new Label()
			{
				Text = "thinktecture",
				HorizontalOptions = LayoutOptions.Center
			};

			// Just don't know why Android crashes when trying to load the image. So we just use text instead. :(
			Content = Device.OnPlatform((View)image, label, image);
		}
	}
}