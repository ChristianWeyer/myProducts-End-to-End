using myProducts.Xamarin.Views.Pages;

namespace myProducts.Xamarin.Views.Contracts
{
	public interface IViewLocator
	{
		LoginPage LoginPage { get; }
		MainPage MainPage { get; }
		ArticlesPage ArticlesPage { get; }
		ArticleDetailPage ArticleDetailPage { get; }
		ArticleMasterPage ArticleMasterPage { get; }
		StatisticsPage StatisticsPage { get; }
		BackgroundNavigationPage BackgroundNavigationPage { get; }
		GalleryPage GalleryPage { get; }
	}
}