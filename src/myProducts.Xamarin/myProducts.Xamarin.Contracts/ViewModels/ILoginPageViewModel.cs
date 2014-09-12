using System.Windows.Input;

namespace myProducts.Xamarin.Contracts.ViewModels
{
	public interface ILoginPageViewModel : IBusyIndicator
	{
		/// <summary>
		/// The actual username
		/// </summary>
		string UserName { get; set; }

		/// <summary>
		/// the actual password
		/// </summary>
		string Password { get; set; }

		/// <summary>
		/// If an error occurs (wrong credentials) will be set to true
		/// </summary>
		bool ErrorOccured { get; set; }

		/// <summary>
		/// Command to be executed when login is requested by the user
		/// </summary>
		ICommand LogInCommand { get; set; }

		/// <summary>
		/// Command to execute when viewmodel wants to navigate to the mainpage
		/// </summary>
		ICommand NavigateToMainPageCommand { get; set; }
	}
}