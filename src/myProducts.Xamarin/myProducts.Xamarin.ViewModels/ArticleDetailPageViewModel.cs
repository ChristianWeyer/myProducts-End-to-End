using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.ViewModels
{
	public class ArticleDetailPageViewModel : BaseViewModel, IArticleDetailPageViewModel
	{
		private readonly IArticlesServiceClient _articlesServiceClient;
		private ArticleDetailDto _articleDetail;
		private string _name;

		public ArticleDetailPageViewModel(IArticlesServiceClient articlesServiceClient)
		{
			_articlesServiceClient = articlesServiceClient;
		}

		public async Task DownloadArticleBy(Guid id)
		{
			IsBusy = true;

			var articleDetail = await _articlesServiceClient.GetArticleBy(id);

			if (articleDetail == null)
			{
				IsBusy = false;
				throw new KeyNotFoundException(String.Format("Article '{0}' not found", id));
			}

			ArticleDetail = articleDetail;
			IsBusy = false;
		}

		public ArticleDetailDto ArticleDetail
		{
			get { return _articleDetail; }
			set { Set(ref _articleDetail, value); }
		}
	}
}