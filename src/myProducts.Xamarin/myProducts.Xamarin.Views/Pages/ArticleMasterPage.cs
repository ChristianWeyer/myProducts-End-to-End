using System;
using myProducts.Xamarin.Contracts.Locale;
using myProducts.Xamarin.Views.Components;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class ArticleMasterPage : ContentPage
	{
		private readonly ITranslation _translation;

		public ArticleMasterPage(ITranslation translation)
		{
			_translation = translation;
			CreateUI();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			Title = _translation.Overview;

			var items = new []
			{
				"Test 1",
				"Test 2",
				"Test 3",
				"Test 4",
				"Test 5",
				"Test 6",
				"Test 7",
				"Test 8",
				"Test 9",
				"Test 10",
				"Test 11",
				"Test 12",
				"Test 1",
				"Test 2",
				"Test 3",
				"Test 4",
				"Test 5",
				"Test 6",
				"Test 7",
				"Test 8",
				"Test 9",
				"Test 10",
				"Test 11",
				"Test 12",
			};

			var listView = new ListView()
			{
				ItemsSource = items,
			};

			Content = new StackLayout()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children =
				{
					listView,
					new Footer(),
				}
			};
		}
	}
}