namespace myProducts.Xamarin.Contracts.Networking
{
	public interface ITokenManager
	{
		string Token { get; }
		void RequestToken(string userName, string password);
	}
}