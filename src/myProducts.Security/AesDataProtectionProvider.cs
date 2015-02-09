using System;
using Microsoft.Owin.Security.DataProtection;
using System.Text;
using System.Security.Cryptography;

namespace MyProducts.Security
{
	public class AesDataProtectorProvider : IDataProtectionProvider
	{
		private string _key;

		/// <summary>
		/// Initializes a new instance of the <see cref="AesDataProtectorProvider"/> class.
		/// </summary>
		/// <param name="key">The key.</param>
		public AesDataProtectorProvider(string key = null)
		{
			_key = key;
		}

		private string SeedHash
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_key))
					_key = HashString(Environment.MachineName);

				return _key;
			}
		}

		/// <summary>
		/// Returns a new instance of IDataProtection for the provider.
		/// </summary>
		/// <param name="purposes">Additional entropy used to ensure protected data may only be unprotected for the correct purposes.</param>
		/// <returns>
		/// An instance of a data protection service
		/// </returns>
		public IDataProtector Create(params string[] purposes)
		{
			return new AesDataProtector(SeedHash);
		}

		private static string HashString(string value)
		{
			var alg = SHA512.Create();
			var result = alg.ComputeHash(Encoding.ASCII.GetBytes(value));
			return HexStringFromBytes(result).ToUpper();
		}

		/// <summary>
		/// Convert an array of bytes to a string of hex digits
		/// </summary>
		/// <param name="bytes">array of bytes</param>
		/// <returns>String of hex digits</returns>
		public static string HexStringFromBytes(byte[] bytes)
		{
			var sb = new StringBuilder();
			foreach (byte b in bytes)
			{
				var hex = b.ToString("x2");
				sb.Append(hex);
			}
			return sb.ToString();
		}
	}
}

