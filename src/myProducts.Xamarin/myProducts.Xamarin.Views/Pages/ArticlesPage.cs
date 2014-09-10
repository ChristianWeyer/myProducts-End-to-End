using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Contracts;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	// Don't use BasePage here, this is just for special purpose
	public class ArticlesPage : MasterDetailPage
	{
		private readonly IViewLocator _viewLocator;

		public ArticlesPage(IViewLocator viewLocator)
		{
			_viewLocator = viewLocator;
			CreateUI();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			Master = CreateMaster();
			Detail = CreateDetail();
		}

		private ArticleDetailPage CreateDetail()
		{
			return _viewLocator.ArticleDetailPage;
		}

		private ArticleMasterPage CreateMaster()
		{
			return _viewLocator.ArticleMasterPage;
		}

		protected override void OnAppearing()
		{
			IsPresented = true;
		}
	}
}