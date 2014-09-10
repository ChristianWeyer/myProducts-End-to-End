using System;
using System.Globalization;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Converters
{
	public class ArticleImageToUriImageSourceConverter : IValueConverter
	{
		private const string ImageUrlTemplate = "https://demo.christianweyer.net/{0}";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var uri = String.Format(ImageUrlTemplate, value);

			return new UriImageSource()
			{
				Uri = new Uri(uri),
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}