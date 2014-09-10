using myProducts.Xamarin.Contracts.ViewModels;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	// Don't use BasePage here, this is just for special purpose
	public class ArticlesPage : MasterDetailPage
	{
		private readonly IArticlesPageViewModel _viewModel;

		public ArticlesPage(IArticlesPageViewModel viewModel)
		{
			_viewModel = viewModel;
			BindingContext = _viewModel;
		}
	}
}