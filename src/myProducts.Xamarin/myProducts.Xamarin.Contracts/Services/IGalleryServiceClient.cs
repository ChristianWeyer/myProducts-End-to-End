using System.Collections.Generic;
using System.Threading.Tasks;

namespace myProducts.Xamarin.Contracts.Services
{
	public interface IGalleryServiceClient
	{
		Task<IEnumerable<string>> GetImages();
	}
}