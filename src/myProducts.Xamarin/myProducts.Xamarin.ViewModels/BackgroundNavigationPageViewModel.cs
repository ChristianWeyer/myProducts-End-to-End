using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.ViewModels
{
	public class BackgroundNavigationPageViewModel : IBackgroundNavigationPageViewModel
	{
		private readonly ITokenManager _tokenManager;

		public BackgroundNavigationPageViewModel(ITokenManager tokenManager)
		{
			_tokenManager = tokenManager;
		}

		public void LogOut()
		{
			_tokenManager.DeleteToken();
		}
	}
}