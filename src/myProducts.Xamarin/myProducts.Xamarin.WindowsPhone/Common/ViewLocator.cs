using System;
using Autofac;
using myProducts.Xamarin.Views.Pages;

namespace myProducts.Xamarin.WindowsPhone.Common
{
	public class ViewLocator : BaseViewLocator
	{
		private static readonly Lazy<ViewLocator> _instance = new Lazy<ViewLocator>(() => new ViewLocator());
 
		public static ViewLocator Instance
		{
			get { return _instance.Value; }
		}


		protected override void WirePlatformDependentServices(ContainerBuilder builder)
		{
			// Currently no platform dependent services
		}
	}
}