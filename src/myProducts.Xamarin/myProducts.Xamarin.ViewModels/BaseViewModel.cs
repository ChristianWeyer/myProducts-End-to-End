using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.ViewModels
{
	public class BaseViewModel : BindableBase, IBusyIndicator
	{
		private bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set { Set(ref _isBusy, value); }
		}
	}
}