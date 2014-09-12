using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace myProducts.Xamarin.Common.Extensions
{
	public static class ObserverableCollectionExtensions
	{
		public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				collection.Add(item);
			}
		}
	}
}