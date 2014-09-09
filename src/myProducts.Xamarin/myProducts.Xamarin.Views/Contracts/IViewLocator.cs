using Autofac;
using myProducts.Xamarin.Views.Pages;

namespace myProducts.Xamarin.Views.Contracts
{
	public interface IViewLocator
	{
		LoginPage LoginPage { get; }
	}
}