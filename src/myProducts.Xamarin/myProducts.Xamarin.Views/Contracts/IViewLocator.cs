using myProducts.Xamarin.Views.Pages;
using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.Views.Contracts
{
	public interface IViewLocator
	{
		LoginPage LoginPage { get; }
		MainPage MainPage { get; }
		ArticleDetailPage ArticleDetailPage { get; }
		ArticleMasterPage ArticleMasterPage { get; }
		StatisticsPage StatisticsPage { get; }
		BackgroundNavigationPage BackgroundNavigationPage { get; }
		GalleryPage GalleryPage { get; }
		InfoPage InfoPage { get; }
		IGalleryPageViewModel GalleryPageViewModel { get; }
	}
}