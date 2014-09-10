using Autofac;
using myProducts.Xamarin.Views.Pages;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Contracts
{
	public interface IViewLocator
	{
		LoginPage LoginPage { get; }
		MainPage MainPage { get; }
		ArticlesPage ArticlesPage { get; }
		ArticleDetailPage ArticleDetailPage { get; }
		ArticleMasterPage ArticleMasterPage { get; }
	}
}