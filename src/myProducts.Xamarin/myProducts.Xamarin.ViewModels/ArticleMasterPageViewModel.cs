using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Common.Extensions;
using myProducts.Xamarin.Contracts.Networking;
using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.ViewModels
{
	public class ArticleMasterPageViewModel : BindableBase, IArticleMasterPageViewModel
	{
		private ObservableCollection<ArticleDto> _items;
		private readonly IArticlesServiceClient _articlesServiceClient;
		private int _pageSize = 10;
		private int _currentPage = 1;
		private bool _isDownloading;

		public ArticleMasterPageViewModel(IArticlesServiceClient articlesServiceClient)
		{
			_articlesServiceClient = articlesServiceClient;
		}

		public ObservableCollection<ArticleDto> Items
		{
			get { return _items ?? (_items = new ObservableCollection<ArticleDto>()); }
			set { Set(ref _items, value); }
		}

		public bool IsDownloading
		{
			get { return _isDownloading; }
			set { Set(ref _isDownloading, value); }
		}

		public async Task DownloadPagedArticles()
		{
			IsDownloading = true;
			
			var data = await _articlesServiceClient.GetPaged(_pageSize, _currentPage, String.Empty);
			Items.AddRange(data);
			
			IsDownloading = false;
		}

		public async Task DownloadMorePagedArticles()
		{
			IsDownloading = true;
	
			var data = await _articlesServiceClient.GetPaged(_pageSize, ++_currentPage, String.Empty);
			Items.AddRange(data);

			IsDownloading = false;
		}
	}
}