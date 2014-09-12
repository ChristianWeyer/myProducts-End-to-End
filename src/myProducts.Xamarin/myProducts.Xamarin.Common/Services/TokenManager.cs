using System;
using System.Threading.Tasks;
using myProducts.Xamarin.Contracts.Services;
using Thinktecture.IdentityModel.Client;

namespace myProducts.Xamarin.Common.Services
{
	public class TokenManager : ITokenManager
	{
		private const string TokenUrl = "https://demo.christianweyer.net/token";
		private string _token;

		public string Token
		{
			get
			{
				return _token;
			}
		}

		public async Task RequestToken(string userName, string password)
		{
			var client = new OAuth2Client(new Uri(TokenUrl));
			var token = await client.RequestResourceOwnerPasswordAsync(userName, password);

			if (token.IsError
			    || token.IsHttpError)
			{
				throw new UnauthorizedAccessException();
			}

			_token = token.AccessToken;
		}
	}
}