using Android.App;
using Android.OS;
using myProducts.Xamarin.Android.Common;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Application(Icon = "@drawable/Icon", Theme = "@android:style/Theme.Holo.Light")]

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

