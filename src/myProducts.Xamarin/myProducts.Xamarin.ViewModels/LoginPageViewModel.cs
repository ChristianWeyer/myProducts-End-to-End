using System;
using System.Threading.Tasks;
using System.Windows.Input;
using myProducts.Xamarin.Contracts.Networking;
using myProducts.Xamarin.Contracts.ViewModels;
using Xamarin.Forms;

namespace myProducts.Xamarin.ViewModels
{
	public class LoginPageViewModel : BindableBase, ILoginPageViewModel
	{
		private readonly ITokenManager _tokenManager;
		private string _userName;
		private string _password;
		private ICommand _logInCommand;
		private bool _errorOccured;

		public LoginPageViewModel(ITokenManager tokenManager)
		{
			_tokenManager = tokenManager;

			LogInCommand = new Command(async() => await LogIn());
		}

		private async Task LogIn()
		{
			try
			{
				await _tokenManager.RequestToken(UserName, Password);
				ErrorOccured = false;
			}
			catch (UnauthorizedAccessException ex)
			{
				ErrorOccured = true;
			}
		}

		public string UserName
		{
			get { return _userName; }
			set { Set(ref _userName, value); }
		}

		public string Password
		{
			get { return _password; }
			set { Set(ref _password, value); }
		}

		public bool ErrorOccured
		{
			get { return _errorOccured; }
			set { Set(ref _errorOccured, value); }
		}

		public ICommand LogInCommand
		{
			get { return _logInCommand; }
			set { Set(ref _logInCommand, value); }
		}
	}
}