using myProducts.Xamarin.Contracts.i18n;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class BackgroundNavigationPage : NavigationPage
	{
		private const string LogOutImageBlack = "logout_black.png";
		private const string LogOutImageWhite = "logout_white.png";

		private readonly IBackgroundNavigationPageViewModel _viewModel;
		private readonly ITranslation _translation;
		private ToolbarItem _logoutToolbarItem;

		public BackgroundNavigationPage(IBackgroundNavigationPageViewModel viewModel, ITranslation translation)
		{
			_viewModel = viewModel;
			_translation = translation;
			CreateToolbarItem();
		}

		public void ShowLogOutButton()
		{
			if (ToolbarItems.Contains(_logoutToolbarItem))
			{
				return;
			}

			ToolbarItems.Add(_logoutToolbarItem);
		}

		private void CreateToolbarItem()
		{
			_logoutToolbarItem = new ToolbarItem(_translation.LogOut, 
				Device.OnPlatform(LogOutImageBlack, LogOutImageBlack, LogOutImageWhite).ToDeviceImage(), 
				async () =>
			{
				_viewModel.LogOut();
				await Navigation.PopToRootAsync();
			});
		}

		public void HideLogOutButton()
		{
			if (!ToolbarItems.Contains(_logoutToolbarItem))
			{
				return;
			}

			ToolbarItems.Remove(_logoutToolbarItem);
		}
	}
}