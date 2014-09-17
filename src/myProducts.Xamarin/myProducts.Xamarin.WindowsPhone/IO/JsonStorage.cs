using System;
using System.IO;
using System.IO.IsolatedStorage;
using myProducts.Xamarin.Common.Text;
using myProducts.Xamarin.Contracts.IO;

namespace myProducts.Xamarin.WindowsPhone.IO
{
	public class JsonStorage : IStorage
	{
		private const string FolderLocation = "JsonStorage";
		private readonly IsolatedStorageFile _storage;

		public JsonStorage()
		{
			BasePath = String.Empty;
			_storage = IsolatedStorageFile.GetUserStoreForApplication();
		}

		public string BasePath { get; set; }

		public void SaveTo<T>(string fileName, T objectToSave)
		{
			var path = GetStoragePath(fileName);
			CreateNecessaryFolders(path);
			using (var isoStream = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, _storage))
			{
				JsonConverter.Serialize(isoStream, objectToSave);
			}
		}

		public T LoadFrom<T>(string fileName)
		{
			try
			{
				using (
					var isoStream = new IsolatedStorageFileStream(GetStoragePath(fileName), FileMode.Open, FileAccess.Read, _storage))
				{
					var obj = JsonConverter.Deserialize<T>(isoStream);
					return obj;
				}
			}
			catch
			{
				return default(T);
			}
		}

		private string GetStoragePath(string fileName)
		{
			var path = Path.Combine(FolderLocation, BasePath, fileName);
			return path;
		}

		public void Remove(string fileName)
		{
			try
			{
				_storage.DeleteFile(GetStoragePath(fileName));
			}
			catch {}
		}

		public bool Exists(string fileName)
		{
			return _storage.FileExists(GetStoragePath(fileName));
		}

		private void CreateNecessaryFolders(string path)
		{
			var directory = Path.GetDirectoryName(path);
			if (directory == null)
			{
				return;
			}

			_storage.CreateDirectory(directory);
		}
	}
}