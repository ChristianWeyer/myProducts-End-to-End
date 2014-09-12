using MyProducts.Services.DTOs;
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

		private void CreateUI()
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
			var masterPage =  _viewLocator.ArticleMasterPage;

			masterPage.ListView.ItemSelected += ListViewItemSelected;

			return masterPage;
		}

		private async void ListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var articleDetailPage = Detail as ArticleDetailPage;
			if (articleDetailPage == null)
			{
				return;
			}

			var item = e.SelectedItem as ArticleDto;
			if (item == null)
			{
				return;
			}


			await articleDetailPage.ViewModel.DownloadArticleBy(item.Id);
			IsPresented = false;
		}

		protected override void OnAppearing()
		{
			IsPresented = true;
		}
	}
}