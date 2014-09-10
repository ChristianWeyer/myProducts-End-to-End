using System;
using System.Threading.Tasks;
using MyProducts.Services.DTOs;

namespace myProducts.Xamarin.Contracts.ViewModels
{
	public interface IArticleDetailPageViewModel : IBusyIndicator
	{
		Task DownloadArticleBy(Guid id);
		ArticleDetailDto ArticleDetail { get; set; }
	}
}