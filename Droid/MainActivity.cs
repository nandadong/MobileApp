using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Gcm.Client;
using Android.Speech;

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
			"serverLocation : \"http://52.1.192.214/\" " +
			"}";
		
		protected override void OnCreate (Bundle savedState)
		{
			Log.Info("GCM-Client", "HELLO");

			base.OnCreate (savedState);

			GcmClient.CheckDevice(this);
			GcmClient.CheckManifest(this);

			//Get the stored latest registration id
			GcmClient.Register(this, MyGCMBroadcastReceiver.SENDER_IDS);
			var registrationId = GcmClient.GetRegistrationId(this);
			Log.Info("GCM-Client", "Registered with this ID: " + registrationId);
			

			Bundle bundle = Intent.Extras;

			if (bundle == null) {
				bundle = new Bundle ();
				bundle.PutString ("MODE", "SIM");
				bundle.PutString ("CONFIG", jsonConfigString);
				bundle.PutString ("TIMELINE", jsonTimelineString);
				bundle.PutString ("USER", "user1");
				bundle.PutString ("PASS", "password");
			}

			global::Xamarin.Forms.Forms.Init (this, savedState);

			LoadApplication (new App (bundle.Get("MODE").ToString(), 
				bundle.Get("CONFIG").ToString(),
				bundle.Get("TIMELINE").ToString(),  
				bundle.Get("USER").ToString(), 
				bundle.Get("PASS").ToString() ));
		}

		protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
		{
			if (requestCode == 10)
			{
				if (resultVal == Result.Ok)
				{
					var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
					if (matches.Count != 0)
					{
						//string textInput = textBox.Text + matches[0];

						// limit the output to 500 characters
						//if (textInput.Length > 500)
						//	textInput = textInput.Substring(0, 500);
						//textBox.Text = textInput;
					}
					//else
						//textBox.Text = "No speech was recognised";
					// change the text back on the button
					//recButton.Text = "Start Recording";
				}
			}

			base.OnActivityResult(requestCode, resultVal, data);
		}
	}
}

