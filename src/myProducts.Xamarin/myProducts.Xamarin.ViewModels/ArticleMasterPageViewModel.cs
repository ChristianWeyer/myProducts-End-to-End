using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Common.Extensions;
using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.ViewModels
{
	public class ArticleMasterPageViewModel : BaseViewModel, IArticleMasterPageViewModel
	{
		private ObservableCollection<ArticleDto> _items;
		private readonly IArticlesServiceClient _articlesServiceClient;
		private int _pageSize = 10;
		private int _currentPage = 1;

		public ArticleMasterPageViewModel(IArticlesServiceClient articlesServiceClient)
		{
			_articlesServiceClient = articlesServiceClient;
		}

		public ObservableCollection<ArticleDto> Items
		{
			get { return _items ?? (_items = new ObservableCollection<ArticleDto>()); }
			set { Set(ref _items, value); }
		}


		public async Task DownloadPagedArticles()
		{
			IsBusy = true;

			_currentPage = 1;
			var data = await _articlesServiceClient.GetPaged(_pageSize, _currentPage, String.Empty);
			Items.AddRange(data);

			IsBusy = false;
		}

		public async Task DownloadMorePagedArticles()
		{
			IsBusy = true;
	
			var data = await _articlesServiceClient.GetPaged(_pageSize, ++_currentPage, String.Empty);
			Items.AddRange(data);

			IsBusy = false;
		}
	}
}