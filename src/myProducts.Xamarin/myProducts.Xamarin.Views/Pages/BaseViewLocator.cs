using Autofac;
using myProducts.Xamarin.Views.Contracts;

namespace myProducts.Xamarin.Views.Pages
{
	public abstract class BaseViewLocator : IViewLocator
	{
		private IContainer _container;

		public BaseViewLocator()
		{
			BuildIoCContainer();
		}

		private void BuildIoCContainer()
		{
			var builder = new ContainerBuilder();

			WirePages(builder);
			WirePlatformDependentServices(builder);

			_container = builder.Build();
		}

		private void WirePages(ContainerBuilder builder)
		{
			builder.RegisterType<LoginPage>();
		}

		public LoginPage LoginPage
		{
			get { return _container.Resolve<LoginPage>(); }
		}

		protected abstract void WirePlatformDependentServices(ContainerBuilder builder);
	}
}