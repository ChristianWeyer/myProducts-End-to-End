using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;

namespace myProducts.Xamarin.Contracts.ViewModels
{
	public interface IArticleMasterPageViewModel : IBusyIndicator
	{
		ObservableCollection<ArticleDto> Items { get; set; }

		Task DownloadPagedArticles();
		Task DownloadMorePagedArticles();
	}
}