using System.Collections.ObjectModel;
using System.Threading.Tasks;
using myProducts.Xamarin.Common.Extensions;
using myProducts.Xamarin.Contracts.Services;
using myProducts.Xamarin.Contracts.ViewModels;

namespace myProducts.Xamarin.ViewModels
{
	public class GalleryPageViewModel : BaseViewModel, IGalleryPageViewModel
	{
		private readonly IGalleryServiceClient _serviceClient;
		private ObservableCollection<string> _images;

		public GalleryPageViewModel(IGalleryServiceClient serviceClient)
		{
			_serviceClient = serviceClient;
		}

		public ObservableCollection<string> Images
		{
			get { return _images ?? (_images = new ObservableCollection<string>()); }
			set { Set(ref _images, value); }
		}

		public async Task DownloadImages()
		{
			IsBusy = true;

			var images = await _serviceClient.GetImages();
			
			Images.AddRange(images);

			IsBusy = false;
		}
	}
}