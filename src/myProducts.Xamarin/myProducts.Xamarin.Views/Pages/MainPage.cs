using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class MainPage : ContentPage
	{
		public MainPage()
		{
			CreateUI();

			this.SetDefaultPadding();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			var gridLayout = CreateGridLayout();

			var articleBox = new MainPageHubItem("Articles", Color.Gray, new Command(() => {}));
			var galleryBox = new MainPageHubItem("Gallery", Color.Blue, new Command(() => {}));
			var logBox = new MainPageHubItem("Logs", Color.Aqua, new Command(() => {}));
			var statisticBox = new MainPageHubItem("Statistics", Color.Navy, new Command(() => { }));
			var infoBox = new MainPageHubItem("Info", Color.Teal, new Command(() => { }));

			AddHubItemToGrid(gridLayout, articleBox, 0, 0);
			AddHubItemToGrid(gridLayout, galleryBox, 0, 1);
			AddHubItemToGrid(gridLayout, logBox, 1, 0);
			AddHubItemToGrid(gridLayout, statisticBox, 1, 1);
			AddHubItemToGrid(gridLayout, infoBox, 2, 0);

			Content = gridLayout;
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