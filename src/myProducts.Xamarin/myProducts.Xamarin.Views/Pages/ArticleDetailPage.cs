using MyProducts.Services.DTOs;
using myProducts.Xamarin.Contracts.i18n;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Converters;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class ArticleDetailPage : BasePage
	{
		private readonly ITranslation _translation;
		private readonly IArticleDetailPageViewModel _viewModel;

		public IArticleDetailPageViewModel ViewModel
		{
			get { return _viewModel; }
		}

		public ArticleDetailPage(ITranslation translation, IArticleDetailPageViewModel viewModel)
		{
			_translation = translation;
			_viewModel = viewModel;
			BindingContext = _viewModel;
			CreateUI();
			this.SetDefaultPadding();
		}

		private void CreateUI()
		{
			var stackLayout = CreateStackLayout();
			var nameBox = CreateNameBox();
			var codeBox = CreateCodeBox();
			var descriptionBox = CreateDescriptionBox();
			var image = CreateImage();

			stackLayout.SetBinding<IArticleDetailPageViewModel>(StackLayout.BindingContextProperty, m => m.ArticleDetail);
			stackLayout.Children.AddRange(nameBox, codeBox, descriptionBox, image);

			SetScrollViewContent(stackLayout);
		}

		private StackLayout CreateNameBox()
		{
			var stackLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			var nameLabel = new Label()
			{
				Text = _translation.Name,
				Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			var nameValueLabel = new Label()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};
			nameValueLabel.SetBinding<ArticleDetailDto>(Label.TextProperty, m => m.Name);

			stackLayout.Children.AddRange(nameLabel, nameValueLabel);

			return stackLayout;
		}

		private StackLayout CreateCodeBox()
		{
			var stackLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			var codeLabel = new Label()
			{
				Text = _translation.Code,
				Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			var codeValueLabel = new Label()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};
			codeValueLabel.SetBinding<ArticleDetailDto>(Label.TextProperty, m => m.Code);

			stackLayout.Children.AddRange(codeLabel, codeValueLabel);

			return stackLayout;
		}

		private StackLayout CreateDescriptionBox()
		{
			var stackLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			var descriptionLabel = new Label()
			{
				Text = _translation.Description,
				Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			var descriptionValueLabel = new Label()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};
			descriptionValueLabel.SetBinding<ArticleDetailDto>(Label.TextProperty, m => m.Description);

			stackLayout.Children.AddRange(descriptionLabel, descriptionValueLabel);

			return stackLayout;
		}

		private Image CreateImage()
		{
			var image = new Image()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Aspect = Aspect.AspectFit,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
			image.SetBinding<ArticleDetailDto>(Image.SourceProperty, m => m.ImageUrl, converter: new ArticleImageToUriImageSourceConverter());

			return image;
		}

		private StackLayout CreateStackLayout()
		{
			return new StackLayout()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};
		}
	}
}