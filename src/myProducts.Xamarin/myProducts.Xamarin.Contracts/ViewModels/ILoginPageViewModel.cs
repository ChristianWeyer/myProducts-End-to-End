using System.Windows.Input;

namespace myProducts.Xamarin.Contracts.ViewModels
{
	public interface ILoginPageViewModel
	{
		string UserName { get; set; }
		string Password { get; set; }

		bool ErrorOccured { get; set; }

		ICommand LogInCommand { get; set; }
	}
}