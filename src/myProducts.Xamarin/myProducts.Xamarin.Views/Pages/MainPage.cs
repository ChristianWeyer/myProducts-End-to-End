using myProducts.Xamarin.Contracts.Locale;
using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Contracts;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class MainPage : BasePage
	{
		private readonly ITranslation _translation;
		private readonly IViewLocator _viewLocator;

		public MainPage(ITranslation translation, IViewLocator viewLocator)
		{
			_translation = translation;
			_viewLocator = viewLocator;
			CreateUI();

			this.SetDefaultPadding();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			var gridLayout = CreateGridLayout();

			var articleBox = new MainPageHubItem(_translation.Articles, Color.White, Color.FromHex("525252"), new Command(async () => await Navigation.PushAsync(_viewLocator.ArticlesPage)));
			var galleryBox = new MainPageHubItem(_translation.Gallery, Color.White, Color.FromHex("2d89ef"), new Command(() => {}));
			var logBox = new MainPageHubItem(_translation.Logs, Color.White, Color.FromHex("2b5797"), new Command(() => {}));
			var statisticBox = new MainPageHubItem(_translation.Statistics, Color.White, Color.FromHex("525252"), new Command(() => { }));
			var infoBox = new MainPageHubItem(_translation.Info, Color.Black, Color.FromHex("d5e7ec"), new Command(() => { }));

			AddHubItemToGrid(gridLayout, articleBox, 0, 0);
			AddHubItemToGrid(gridLayout, galleryBox, 0, 1);
			AddHubItemToGrid(gridLayout, logBox, 1, 0);
			AddHubItemToGrid(gridLayout, statisticBox, 1, 1);
			AddHubItemToGrid(gridLayout, infoBox, 2, 0);

			SetScrollViewContent(gridLayout);
		}

		private void AddHubItemToGrid(Grid grid, MainPageHubItem item, int row, int column)
		{
			grid.Children.Add(item.BoxView, column, row);
			grid.Children.Add(item.Label, column, row);
		}

		private Grid CreateGridLayout()
		{
			var grid = new Grid()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowSpacing = 15,
				ColumnSpacing = 15,
				ColumnDefinitions =
				{
					new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)},
					new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)},
				},
				RowDefinitions =
				{
					new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)},
					new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)},
					new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)},
				},
			};

			return grid;
		}
	}
}