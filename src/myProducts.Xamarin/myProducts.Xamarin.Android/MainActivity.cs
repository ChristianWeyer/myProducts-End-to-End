using Android.App;
using Android.OS;
using myProducts.Xamarin.Android.Common;
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

			SetPage(new NavigationPage(ViewLocator.Instance.LoginPage));
		}
	}
}

