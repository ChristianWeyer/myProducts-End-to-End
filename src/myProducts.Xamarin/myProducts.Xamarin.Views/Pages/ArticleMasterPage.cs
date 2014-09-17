using System.Linq;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.i18n;
using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Contracts;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class ArticleMasterPage : ContentPage
	{
		private readonly ITranslation _translation;
		private readonly IArticleMasterPageViewModel _viewModel;
		private readonly IArticlesHubProxy _articlesHub;
		private readonly IViewLocator _viewLocator;

		public ArticleMasterPage(ITranslation translation, 
			IArticleMasterPageViewModel viewModel,
			IArticlesHubProxy articlesHub,
			IViewLocator viewLocator)
		{
			_translation = translation;
			_viewModel = viewModel;
			_articlesHub = articlesHub;
			_viewLocator = viewLocator;
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
			
			var listView = new ListView()
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

			listView.ItemAppearing += ListViewItemAppearing;
			listView.ItemSelected += ListViewItemSelected;
			listView.SetBinding<IArticleMasterPageViewModel>(ListView.ItemsSourceProperty, m => m.Items);

			return listView;
		}

		private void ListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var article = e.SelectedItem as ArticleDto;

			if (article == null)
			{
				return;
			}

			var articleDetailPage = _viewLocator.ArticleDetailPage;
			// We don't need to await here, since we can download while navigating to the page
			articleDetailPage.ViewModel.DownloadArticleBy(article.Id);
			Navigation.PushAsync(articleDetailPage);
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

			// Don't await signalr connection here, since it is not necessary.
			// It can connect while the user views the page
		  _articlesHub.Start();
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