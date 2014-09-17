namespace myProducts.Xamarin.Contracts.IO
{
	public interface IStorage
	{
		string BasePath { get; set; }

		void SaveTo<T>(string fileName, T objectToSave);

		/// <summary>
		///   Should return null when something went wrong
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileName"></param>
		/// <returns></returns>
		T LoadFrom<T>(string fileName);

		bool Exists(string fileName);

		void Remove(string fileName);
	}
}