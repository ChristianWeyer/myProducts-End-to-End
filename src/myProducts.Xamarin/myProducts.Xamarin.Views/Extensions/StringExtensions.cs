using System;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Extensions
{
	public static class StringExtensions
	{
		private const string WPAssetFolderTemplate = "Assets/{0}";

		public static string ToDeviceImage(this string imageFileName)
		{
			return Device.OnPlatform(imageFileName, imageFileName, String.Format(WPAssetFolderTemplate, imageFileName));
		}
	}
}