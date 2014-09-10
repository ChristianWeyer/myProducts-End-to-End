using System;
using myProducts.Xamarin.Contracts.Networking;
using Thinktecture.IdentityModel.Client;

namespace myProducts.Xamarin.Common.Networking
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

		public void RequestToken(string userName, string password)
		{
			var client = new OAuth2Client(new Uri(TokenUrl));
			var token = client.RequestResourceOwnerPasswordAsync("cw", "cw").Result;

			if (token.IsError
			    || token.IsHttpError)
			{
				throw new UnauthorizedAccessException();
			}

			_token = token.AccessToken;
		}
	}
}