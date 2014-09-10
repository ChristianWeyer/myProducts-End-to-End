namespace myProducts.Xamarin.Contracts.Locale
{
	public interface ITranslation
	{
		string IsoCode { get;}
		bool IsDefault { get; }

		string UserLogin { get; }
		string UserName { get; }
		string Password { get; }
		string LogIn { get; }
		string LogInNotPossible { get; }
	}
}