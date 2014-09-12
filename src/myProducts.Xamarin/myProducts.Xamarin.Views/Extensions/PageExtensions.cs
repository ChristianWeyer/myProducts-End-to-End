using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Extensions
{
	public static class PageExtensions
	{
		public static void SetDefaultPadding(this Page page)
		{
			page.Padding = new Thickness(15);
		}
	}
}