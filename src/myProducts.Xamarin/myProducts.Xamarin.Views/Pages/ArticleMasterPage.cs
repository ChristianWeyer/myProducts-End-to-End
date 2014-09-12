using System.Linq;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.i18n;
using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class ArticleMasterPage : ContentPage
	{
		private readonly ITranslation _translation;
		private readonly IArticleMasterPageViewModel _viewModel;
		private readonly IArticlesHubProxy _articlesHub;
		private ListView _listView;

		public ArticleMasterPage(ITranslation translation, 
			IArticleMasterPageViewModel viewModel,
			IArticlesHubProxy articlesHub)
		{
			_translation = translation;
			_viewModel = viewModel;
			_articlesHub = articlesHub;
			BindingContext = _viewModel;
			CreateUI();
			this.SetDefaultPadding();
		}

		private void CreateUI()
		{
			Title = _translation.Overview;

			this.SetBinding<IArticleMasterPageViewModel>(IsBusyProperty, m => m.IsBusy);

			var listView = CreateListView();

			Content = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children =
				{
					listView,
					new Footer(),
				}
			};
		}

		private ListView CreateListView()
		{
			
			ListView = new ListView()
			{
				RowHeight = 80,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				ItemTemplate = new DataTemplate(() =>
				{
					var stackLayout = new StackLayout()
					{
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.Start
					};

					var nameLabel = new Label()
					{
						Font = Font.SystemFontOfSize(NamedSize.Medium),
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.Start,
					};
					nameLabel.SetBinding<ArticleDto>(Label.TextProperty, m => m.Name);

					var codeLabel = new Label()
					{
						Font = Font.SystemFontOfSize(NamedSize.Small),
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.Start,
					};
					codeLabel.SetBinding<ArticleDto>(Label.TextProperty, m => m.Code);

					stackLayout.Children.AddRange(nameLabel, codeLabel);

					return new ViewCell()
					{
						View = stackLayout,
					};
				}),
			};

			ListView.ItemAppearing += ListViewItemAppearing;

			ListView.SetBinding<IArticleMasterPageViewModel>(ListView.ItemsSourceProperty, m => m.Items);
			return ListView;
		}

		public ListView ListView
		{
			get { return _listView; }
			private set { _listView = value; }
		}

		private async void ListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			var item = e.Item as ArticleDto;

			if ((item != null)
				&& (item.Equals(_viewModel.Items.Last())))
			{
				await _viewModel.DownloadMorePagedArticles();
			}
		}

		protected async override void OnAppearing()
		{
			await _viewModel.DownloadPagedArticles();
			_articlesHub.OnArticleChanged += ArticleChanged;
			await _articlesHub.Start();
		}

		private async void ArticleChanged()
		{
			_viewModel.Items.Clear();
			await _viewModel.DownloadPagedArticles();
		}

		protected override void OnDisappearing()
		{
			_articlesHub.OnArticleChanged -= ArticleChanged;
			_articlesHub.Stop();
		}
	}
}