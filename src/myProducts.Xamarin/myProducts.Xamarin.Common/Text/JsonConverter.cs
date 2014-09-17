using System.IO;
using Newtonsoft.Json;

namespace myProducts.Xamarin.Common.Text
{
	public static class JsonConverter
	{
		private static JsonSerializer CreateJsonConverter()
		{
			var settings = GetSerializerSettings();
			return JsonSerializer.Create(settings);
		}

		private static JsonSerializerSettings GetSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				TypeNameHandling = TypeNameHandling.All,
			};
		}

		public static T Deserialize<T>(string input)
		{
			using (var textReader = new StringReader(input))
			{
				using (var jsonReader = new JsonTextReader(textReader))
				{
					var converter = CreateJsonConverter();
					return converter.Deserialize<T>(jsonReader);
				}
			}
		}

		public static T Deserialize<T>(Stream input)
		{
			using (var streamReader = new StreamReader(input))
			{
				string content = streamReader.ReadToEnd();

				return Deserialize<T>(content);
			}
		}

		public static string Serialize<T>(T obj)
		{
			using (var textWriter = new StringWriter())
			{
				using (var jsonWriter = new JsonTextWriter(textWriter))
				{
					var converter = CreateJsonConverter();

					converter.Serialize(jsonWriter, obj);
				}

				textWriter.Flush();

				return textWriter.ToString();
			}
		}

		public static void Serialize<T>(Stream stream, T obj)
		{
			var streamWriter = new StreamWriter(stream);
			string content = Serialize(obj);

			streamWriter.Write(content);
			streamWriter.Flush();
			stream.Seek(0, SeekOrigin.Begin);
		}
	}
}