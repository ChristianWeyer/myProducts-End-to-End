using System.Threading.Tasks;

namespace myProducts.Xamarin.Contracts.Networking
{
	public interface ITokenManager
	{
		string Token { get; }
		Task RequestToken(string userName, string password);
	}
}