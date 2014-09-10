using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;

namespace myProducts.Xamarin.Contracts.ViewModels
{
	public interface IArticleMasterPageViewModel
	{
		ObservableCollection<ArticleDto> Items { get; set; }

		bool IsDownloading { get; set; }

		Task DownloadPagedArticles();
		Task DownloadMorePagedArticles();
	}
}