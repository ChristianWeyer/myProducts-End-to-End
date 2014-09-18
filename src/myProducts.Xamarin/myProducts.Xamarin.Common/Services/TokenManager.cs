using System;
using System.Threading.Tasks;
using myProducts.Xamarin.Contracts.IO;
using myProducts.Xamarin.Contracts.Services;
using Newtonsoft.Json;
using Thinktecture.IdentityModel.Client;

namespace myProducts.Xamarin.Common.Services
{
	public class TokenManager : ITokenManager
	{
		[JsonObject]
		private class InternalTokenStorageItem
		{
			[JsonProperty]
			internal string Token { get; set; }

			[JsonProperty]
			internal DateTime AbsoluteExpirationDate { get; set; }
		}

		private const string TokenUrl = "https://demo.christianweyer.net/token";
		private const string StorageBasePath = "TokenManager";
		private const string StorageFileName = "Token.json";
		private readonly IStorage _storage;
		private string _token;

		public TokenManager(IStorage storage)
		{
			_storage = storage;
			_storage.BasePath = StorageBasePath;

			TryReadTokenFromLocalStorage();
		}

		private void TryReadTokenFromLocalStorage()
		{
			var token = _storage.LoadFrom<InternalTokenStorageItem>(StorageFileName);

			if (token == null)
			{
				return;
			}

			if (DeleteTokenIfExpired(token))
			{
				return;
			}

			_token = token.Token;
		}

		private bool DeleteTokenIfExpired(InternalTokenStorageItem token)
		{
			if (DateTime.Now > token.AbsoluteExpirationDate)
			{
				DeleteToken();
				return true;
			}

			return false;
		}

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
			SaveTokenToLocalStorage(token);
		}

		private void SaveTokenToLocalStorage(TokenResponse token)
		{
			var storageItem = new InternalTokenStorageItem()
			{
				Token = token.AccessToken,
				AbsoluteExpirationDate = DateTime.Now.AddMilliseconds(token.ExpiresIn * 1000),
			};

			_storage.SaveTo(StorageFileName, storageItem);
		}

		public void DeleteToken()
		{
			_storage.Remove(StorageFileName);
			_token = String.Empty;
		}
	}
}