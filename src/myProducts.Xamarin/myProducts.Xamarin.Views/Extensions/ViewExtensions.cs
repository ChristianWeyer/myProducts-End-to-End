using System.Collections.Generic;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Extensions
{
	public static class ViewExtensions
	{
		public static void AddRange(this IList<View> children, params View[] views)
		{
			foreach (var view in views)
			{
				children.Add(view);
			}
		}
	}
}