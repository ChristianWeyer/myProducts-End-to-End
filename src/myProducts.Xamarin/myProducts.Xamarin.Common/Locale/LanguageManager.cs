using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using myProducts.Xamarin.Contracts.Locale;

namespace myProducts.Xamarin.Common.Locale
{
	public class LanguageManager : ILanguageManager
	{
		private readonly Dictionary<string, ITranslation> _translations = new Dictionary<string, ITranslation>();

		private void EnsureOnlyOneDefaultLanguage(ITranslation translationToAdd)
		{
			if ((translationToAdd.IsDefault) 
				&& (_translations.Any(t => t.Value.IsDefault)))
			{
				throw new Exception("Only one default langauge is allowed.");
			}
		}

		private void EnsureTranslationIsNotADuplicate(ITranslation translationToAdd)
		{
			if (_translations.ContainsKey(translationToAdd.IsoCode))
			{
				throw new Exception(String.Format("{0} has already been added as a translation", translationToAdd.IsoCode));
			}
		}

		public void AddTranslation(ITranslation translation)
		{
			EnsureOnlyOneDefaultLanguage(translation);
			EnsureTranslationIsNotADuplicate(translation);

			_translations.Add(translation.IsoCode, translation);
		}

		public ITranslation GetCurrentTranslation()
		{
			var culture = CultureInfo.CurrentUICulture;
			var twoLetterIsoCode = culture.TwoLetterISOLanguageName;

			ITranslation tmp;

			if (_translations.TryGetValue(twoLetterIsoCode, out tmp))
			{
				return tmp;
			}

			var defaultTranslation = _translations.SingleOrDefault(t => t.Value.IsDefault);

			if (defaultTranslation.Equals(new KeyValuePair<string, ITranslation>()))
			{
				throw new Exception("No default language found. App can't not be translated.");
			}

			return defaultTranslation.Value;
		}
	}
}