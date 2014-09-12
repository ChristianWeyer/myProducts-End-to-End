using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using myProducts.Xamarin.Contracts.Networking;
using Newtonsoft.Json;

namespace myProducts.Xamarin.Common.Networking
{
	public class BaseServiceClient
	{
		private readonly ITokenManager _tokenManager;
		private const string ServiceUrl = "https://demo.christianweyer.net/api/";

		protected readonly HttpClient HttpClient;

		public BaseServiceClient(ITokenManager tokenManager)
		{
			_tokenManager = tokenManager;
			HttpClient = new HttpClient
			{
				BaseAddress = new Uri(ServiceUrl),
				DefaultRequestHeaders =
				{
					CacheControl = new CacheControlHeaderValue()
					{
						NoCache = true,
					},
					Pragma =
					{
						new NameValueHeaderValue("Pragma", "no-cache"),
					},
				}
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
			var result = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(result);
		}
	}
}