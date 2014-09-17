using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using myProducts.Xamarin.Contracts.Services;

namespace myProducts.Xamarin.Common.Services
{
	public class GalleryServiceClient : BaseServiceClient, IGalleryServiceClient
	{
		private const string ImagesApiEndpoint = "images";

		public GalleryServiceClient(ITokenManager tokenManager) : base(tokenManager) {}
		
		public async Task<IEnumerable<string>> GetImages()
		{
			var reponse = await Get<IEnumerable<string>>(ImagesApiEndpoint);
			return reponse;
		}
	}
}