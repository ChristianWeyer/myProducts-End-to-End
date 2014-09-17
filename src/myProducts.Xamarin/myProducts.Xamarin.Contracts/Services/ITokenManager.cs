using System;
using System.Threading.Tasks;

namespace myProducts.Xamarin.Contracts.Services
{
	/// <summary>
	/// Manages the OAuth2 token
	/// </summary>
	public interface ITokenManager
	{
		/// <summary>
		/// The actual token
		/// </summary>
		string Token { get; }

		/// <summary>
		/// Requests the token with given <paramref name="userName"/> and <paramref name="password"/>.
		/// After completion it will set <see cref="Token"/>
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		/// <exception cref="UnauthorizedAccessException">Will throw, if credentials are wrong</exception>
		Task RequestToken(string userName, string password);

		/// <summary>
		/// Deletes the token (aka sign out)
		/// </summary>
		void DeleteToken();
	}
}