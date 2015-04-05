using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace HomeAutomationApp.Droid
{
	[Activity (Label = "HomeAutomationApp.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle savedState)
		{
			base.OnCreate (savedState);

			Bundle bundle = Intent.Extras;

			if (bundle == null) {
				bundle = new Bundle ();
				bundle.PutString ("MODE", "SIM");
				bundle.PutString ("CONFIG", "1");
				bundle.PutString ("TIMELINE", "2");
				bundle.PutString ("USER", "3");
				bundle.PutString ("PASS", "4");
			}

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App (bundle.Get("MODE").ToString(), bundle.Get("CONFIG").ToString(),
				bundle.Get("TIMELINE").ToString(),  bundle.Get("USER").ToString(), bundle.Get("PASS").ToString() ));
		}
	}
}

