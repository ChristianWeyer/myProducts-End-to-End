using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace myProducts.Xamarin.Contracts.ViewModels
{
	public interface IGalleryPageViewModel : IBusyIndicator
	{
		ObservableCollection<string> Images { get; }
		Task DownloadImages();
	}
}