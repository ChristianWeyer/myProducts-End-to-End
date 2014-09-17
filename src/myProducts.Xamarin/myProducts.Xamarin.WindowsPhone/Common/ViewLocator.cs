using System;
using Autofac;
using myProducts.Xamarin.Contracts.IO;
using myProducts.Xamarin.Views.Pages;
using myProducts.Xamarin.WindowsPhone.IO;

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
			builder.RegisterType<JsonStorage>()
				.As<IStorage>();
		}
	}
}