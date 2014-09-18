using System;
using Autofac;
using myProducts.Xamarin.Android.IO;
using myProducts.Xamarin.Android.Text;
using myProducts.Xamarin.Contracts.IO;
using myProducts.Xamarin.Contracts.Text;
using myProducts.Xamarin.Views.Pages;

namespace myProducts.Xamarin.Android.Common
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

			builder.RegisterType<MailComposer>()
				.As<IMailComposer>();
		}
	}
}