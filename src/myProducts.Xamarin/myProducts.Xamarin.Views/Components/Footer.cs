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

			Content = new Image()
			{
				Source = ThinktectureLogoFileName.ToDeviceImage(),
				HorizontalOptions = LayoutOptions.Center
			};
		}
	}
}