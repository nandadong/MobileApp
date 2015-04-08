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
		// JSON timeline information
		const string jsonTimelineString =
			"{" +
			"	timeFrame : {" +
			"	    wall: \"1997-07-16T19:20:30+01:00\"," +
			"	    sim: \"1997-07-16T19:20:30+01:00\"," +
			"	    rate: 2.0" +
			"   }," +
			"	events : [" +
			"		{" +
			"			time : \"1997-07-16T19:20:30+01:00\"," +
			"			key : \"locationChange\"," +
			"		value : {" +
			"				lat : 1.123456," +
			"				lon : 2.123456," +
			"				alt : 3.123456" +
			"		}" +
			"		}" +
			"	]" +
			"}";

		const string jsonConfigString = 
			"{" +
				"serverLocation : \"5574serverapi.azurewebsites.net\" " +
			"}";
		
		protected override void OnCreate (Bundle savedState)
		{
			base.OnCreate (savedState);

			Bundle bundle = Intent.Extras;

			if (bundle == null) {
				bundle = new Bundle ();
				bundle.PutString ("MODE", "SIM");
				bundle.PutString ("CONFIG", jsonConfigString);
				bundle.PutString ("TIMELINE", jsonTimelineString);
				bundle.PutString ("USER", "user1");
				bundle.PutString ("PASS", "password");
			}

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App (bundle.Get("MODE").ToString(), bundle.Get("CONFIG").ToString(),
				bundle.Get("TIMELINE").ToString(),  bundle.Get("USER").ToString(), bundle.Get("PASS").ToString() ));
		}
	}
}

