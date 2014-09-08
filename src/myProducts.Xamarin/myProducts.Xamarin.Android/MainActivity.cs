using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace myProducts.Xamarin.Android
{
	[Activity(Label = "myProducts.Xamarin.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
		}
	}
}

