using Autofac;
using myProducts.Xamarin.Common.Locale;
using myProducts.Xamarin.Common.Locale.Languages;
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
			WireLanguage(builder);
			WirePlatformDependentServices(builder);

			_container = builder.Build();
		}

		private void WireLanguage(ContainerBuilder builder)
		{
			builder.Register(context =>
			{
				var languageManager = new LanguageManager();
				languageManager.AddTranslation(new EnglishTranslation());
				languageManager.AddTranslation(new GermanTranslation());

				return languageManager.GetCurrentTranslation();
			});
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