namespace myProducts.Xamarin.Contracts.Locale
{
	public interface ILanguageManager
	{
		void AddTranslation(ITranslation translation);
		ITranslation GetCurrentTranslation();
	}
}