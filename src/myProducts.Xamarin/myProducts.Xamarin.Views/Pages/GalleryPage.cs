using System;
using System.ServiceModel;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Components;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class GalleryPage : CarouselPage
	{
		private const string ImageUriTemplate = "https://demo.christianweyer.net/{0}";
		private readonly IGalleryPageViewModel _viewModel;

		public GalleryPage(IGalleryPageViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		protected async override void OnAppearing()
		{
			await _viewModel.DownloadImages();

			// We neither can bind the children nor the content property of the child pages.
			// So we have to generate it.

			BatchBegin();
			Children.Clear();

			foreach (var image in _viewModel.Images)
			{
				Children.Add(CreateImagePage(image));
			}
			BatchCommit();
		}

		private ContentPage CreateImagePage(string image)
		{
			var page = new ContentPage()
			{
				Content = new StackLayout()
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Children =
					{
						new Image()
						{
							Source = String.Format(ImageUriTemplate, image),
							Aspect = Aspect.AspectFit,
							VerticalOptions = LayoutOptions.FillAndExpand,
							HorizontalOptions = LayoutOptions.FillAndExpand,
						},
						// Yep, footer will scroll new too, but no chance to combine carouselpage with fixed footer
						new Footer()
					}
				}
			};

			return page;
		}
	}
}