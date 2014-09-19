using Autofac;
using myProducts.Xamarin.Common.i18n;
using myProducts.Xamarin.Common.i18n.Languages;
using myProducts.Xamarin.Common.Services;
using myProducts.Xamarin.Contracts.Services;
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

			builder.RegisterType<StatisticsPageViewModel>()
				.As<IStatisticsPageViewModel>();

			builder.RegisterType<BackgroundNavigationPageViewModel>()
				.As<IBackgroundNavigationPageViewModel>();

			builder.RegisterType<GalleryPageViewModel>()
				.As<IGalleryPageViewModel>();

			builder.RegisterType<InfoPageViewModel>()
				.As<IInfoPageViewModel>();
		}

		private void WireServices(ContainerBuilder builder)
		{
			builder.RegisterType<TokenManager>()
				.As<ITokenManager>()
				.SingleInstance();

			builder.RegisterType<ArticlesServiceClient>()
				.As<IArticlesServiceClient>()
				.SingleInstance();

			builder.RegisterType<ArticlesHubProxy>()
				.As<IArticlesHubProxy>();

			builder.RegisterType<StatisticsServiceClient>()
				.As<IStatisticsServiceClient>();

			builder.RegisterType<GalleryServiceClient>()
				.As<IGalleryServiceClient>();
		}

		private void WireLanguage(ContainerBuilder builder)
		{
			builder.Register(context =>
			{
				var languageManager = new LanguageManager();
				languageManager.AddTranslation(new EnglishTranslations());
				languageManager.AddTranslation(new GermanTranslations());

				return languageManager.GetCurrentTranslation();
			});
		}

		private void WirePages(ContainerBuilder builder)
		{
			builder.RegisterType<LoginPage>();
			builder.RegisterType<MainPage>();
			builder.RegisterType<ArticleDetailPage>();
			builder.RegisterType<ArticleMasterPage>();
			builder.RegisterType<StatisticsPage>();
			builder.RegisterType<BackgroundNavigationPage>();
			builder.RegisterType<GalleryPage>();
			builder.RegisterType<InfoPage>();
		}

		public LoginPage LoginPage
		{
			get { return _container.Resolve<LoginPage>(); }
		}

		public MainPage MainPage
		{
			get { return _container.Resolve<MainPage>(); }
		}

		public ArticleDetailPage ArticleDetailPage
		{
			get { return _container.Resolve<ArticleDetailPage>(); }
		}

		public ArticleMasterPage ArticleMasterPage
		{
			get { return _container.Resolve<ArticleMasterPage>(); }
		}

		public StatisticsPage StatisticsPage
		{
			get { return _container.Resolve<StatisticsPage>(); }
		}

		public BackgroundNavigationPage BackgroundNavigationPage
		{
			get { return _container.Resolve<BackgroundNavigationPage>(); }
		}

		public GalleryPage GalleryPage { 
			get { return _container.Resolve<GalleryPage>(); } }

		public InfoPage InfoPage
		{
			get { return _container.Resolve<InfoPage>(); }
		}

		public IGalleryPageViewModel GalleryPageViewModel 
		{
			get { return _container.Resolve<IGalleryPageViewModel> (); }
		}

		protected abstract void WirePlatformDependentServices(ContainerBuilder builder);
	}
}