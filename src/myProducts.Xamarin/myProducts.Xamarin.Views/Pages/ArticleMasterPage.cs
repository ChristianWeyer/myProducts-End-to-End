using System;
using System.Collections.Generic;
using System.Linq;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.Locale;
using myProducts.Xamarin.Contracts.Networking;
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

		public ArticleMasterPage(ITranslation translation, IArticleMasterPageViewModel viewModel)
		{
			_translation = translation;
			_viewModel = viewModel;
			BindingContext = _viewModel;
			CreateUI();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			Title = _translation.Overview;

			var listView = CreateListView();
			var activityIndicator = CreateActivityIndicator();

			Content = new StackLayout()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children =
				{
					listView,
					activityIndicator,
					new Footer(),
				}
			};
		}

		private ActivityIndicator CreateActivityIndicator()
		{
			var indicator = new ActivityIndicator()
			{
				IsRunning = false,
			};
			indicator.SetBinding<IArticleMasterPageViewModel>(ActivityIndicator.IsRunningProperty, m => m.IsDownloading);

			return indicator;
		}

		private ListView CreateListView()
		{
			var listView = new ListView()
			{
				RowHeight = 80,
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
						VerticalOptions = LayoutOptions.Start,
					};
					nameLabel.SetBinding<ArticleDto>(Label.TextProperty, m => m.Name);

					var codeLabel = new Label()
					{
						Font = Font.SystemFontOfSize(NamedSize.Small),
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

			listView.SetBinding<IArticleMasterPageViewModel>(ListView.ItemsSourceProperty, m => m.Items);
			return listView;
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
		}
	}
}