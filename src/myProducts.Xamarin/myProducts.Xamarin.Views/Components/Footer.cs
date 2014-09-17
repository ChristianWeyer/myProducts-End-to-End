using System;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Components
{
	public class Footer : ContentView
	{
		private const string ThinktectureLogoFileName = "thinktecture_logo.png";
		private const string WPAssetFolderTemplate = "Assets/{0}";

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
				Source = Device.OnPlatform(ThinktectureLogoFileName, ThinktectureLogoFileName, String.Format(WPAssetFolderTemplate, ThinktectureLogoFileName)),
				HorizontalOptions = LayoutOptions.Center
			};
		}
	}
}