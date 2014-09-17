using System;
using System.IO;
using myProducts.Xamarin.Common.Text;
using myProducts.Xamarin.Contracts.IO;

namespace myProducts.Xamarin.iOS.IO
{
	public class JsonStorage : IStorage
	{
		public JsonStorage()
		{
			BasePath = String.Empty;
		}

		public string BasePath { get; set; }

		public void SaveTo<T>(string fileName, T objectToSave)
		{
			var path = GetStoragePath(fileName);
			CreateNecessaryFolders(path);
			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				JsonConverter.Serialize(fileStream, objectToSave);
			}
		}

		public T LoadFrom<T>(string fileName)
		{
			try
			{
				if (!Exists(fileName))
				{
					return default(T);
				}

				var path = GetStoragePath(fileName);
				using (var fileStream = new FileStream(path, FileMode.Open))
				{
					var obj = JsonConverter.Deserialize<T>(fileStream);
					return obj;
				}
			}
			catch
			{
				return default(T);
			}
		}

		public bool Exists(string fileName)
		{
			return File.Exists(GetStoragePath(fileName));
		}

		public void Remove(string fileName)
		{
			if (!Exists(fileName))
			{
				return;
			}

			File.Delete(GetStoragePath(fileName));
		}

		private string GetStoragePath(string fileName)
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var cacheFolder = Path.Combine(documents, "..", "Library", "Cache", "JsonStorage", BasePath, fileName);
			return cacheFolder;
		}

		private void CreateNecessaryFolders(string path)
		{
			var directory = Path.GetDirectoryName(path);
			if (directory == null)
			{
				return;
			}

			Directory.CreateDirectory(directory);
		}
	}
}