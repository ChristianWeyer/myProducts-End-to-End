using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace myProducts.Xamarin.Common.Services
{
    [JsonObject]
    [DataContract]
	public class PageResult<T> : IEnumerable<T>, IEnumerable
	{
        [DataMember]
		public IEnumerable<T> Items { get; set; }

        [DataMember]
		public long? Count { get; set; }

        [DataMember]
		public Uri NextPageLink { get; set; }

		public PageResult(IEnumerable<T> items, Uri nextPageLink, long? count)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}

			Items = items;
			NextPageLink = nextPageLink;
			Count = count;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}