using System;
using System.Net.Http;
using System.Threading.Tasks;
using myProducts.Xamarin.Contracts.Networking;
using Newtonsoft.Json;

namespace myProducts.Xamarin.Common.Networking
{
	public class BaseServiceClient
	{
		private readonly ITokenManager _tokenManager;
		private const string ServiceUrl = "https://demo.christianweyer.net/api";

		protected readonly HttpClient HttpClient;

		public BaseServiceClient(ITokenManager tokenManager)
		{
			_tokenManager = tokenManager;
			HttpClient = new HttpClient
			{
				BaseAddress = new Uri(ServiceUrl)
			};
		}

		protected virtual void EnsureTokenIsSet()
		{
			if (HttpClient.DefaultRequestHeaders.Authorization == null)
			{
				HttpClient.SetBearerToken(_tokenManager.Token);
			}
		}

		protected virtual async Task<T> Get<T>(string endPoint)
		{
			EnsureTokenIsSet();
			var response = await HttpClient.GetAsync(endPoint);
			var resultString = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(resultString);
		}
	}
}