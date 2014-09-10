namespace myProducts.Xamarin.Contracts.Locale
{
	public interface ILanguageManager
	{
		/// <summary>
		/// Adds a <see cref="ITranslation"/> to the manager, so it can be used using <see cref="GetCurrentTranslation"/>
		/// </summary>
		/// <param name="translation"></param>
		void AddTranslation(ITranslation translation);

		/// <summary>
		/// Returns the translation for the current language the user has set for his device
		/// </summary>
		/// <returns></returns>
		ITranslation GetCurrentTranslation();
	}
}