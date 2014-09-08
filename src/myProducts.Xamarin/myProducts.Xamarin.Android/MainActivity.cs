using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using myProducts.Xamarin.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace myProducts.Xamarin.Android
{
	[Activity(Label = "myProducts.Xamarin.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Forms.Init(this, bundle);

			var mainPage = PageLocator.MainPage;
			SetPage(new NavigationPage(mainPage));
		}
	}
}

