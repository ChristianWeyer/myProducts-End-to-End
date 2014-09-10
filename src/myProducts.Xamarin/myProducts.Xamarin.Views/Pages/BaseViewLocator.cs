using Autofac;
using myProducts.Xamarin.Common.Locale;
using myProducts.Xamarin.Common.Locale.Languages;
using myProducts.Xamarin.Common.Networking;
using myProducts.Xamarin.Contracts.Networking;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.ViewModels;
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
			WireViewModels(builder);
			WireLanguage(builder);
			WireServices(builder);
			WirePlatformDependentServices(builder);
			WireViewLocator(builder);

			_container = builder.Build();
		}

		private void WireViewLocator(ContainerBuilder builder)
		{
			builder.Register(context => this)
				.As<IViewLocator>()
				.SingleInstance();
		}

		private void WireViewModels(ContainerBuilder builder)
		{
			builder.RegisterType<LoginPageViewModel>()
				.As<ILoginPageViewModel>();

			builder.RegisterType<ArticleMasterPageViewModel>()
				.As<IArticleMasterPageViewModel>();

			builder.RegisterType<ArticleDetailPageViewModel>()
				.As<IArticleDetailPageViewModel>();
		}

		private void WireServices(ContainerBuilder builder)
		{
			builder.RegisterType<TokenManager>()
				.As<ITokenManager>()
				.SingleInstance();

			builder.RegisterType<ArticlesServiceClient>()
				.As<IArticlesServiceClient>()
				.SingleInstance();
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
			builder.RegisterType<MainPage>();
			builder.RegisterType<ArticlesPage>();
			builder.RegisterType<ArticleDetailPage>();
			builder.RegisterType<ArticleMasterPage>();
		}

		public LoginPage LoginPage
		{
			get { return _container.Resolve<LoginPage>(); }
		}

		public MainPage MainPage
		{
			get { return _container.Resolve<MainPage>(); }
		}

		public ArticlesPage ArticlesPage
		{
			get { return _container.Resolve<ArticlesPage>(); }
		}

		public ArticleDetailPage ArticleDetailPage
		{
			get { return _container.Resolve<ArticleDetailPage>(); }
		}

		public ArticleMasterPage ArticleMasterPage
		{
			get { return _container.Resolve<ArticleMasterPage>(); }
		}

		protected abstract void WirePlatformDependentServices(ContainerBuilder builder);
	}
}