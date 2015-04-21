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

using api;
using System.Collections.Generic;
using Toasts.Forms.Plugin.Droid;

namespace HomeAutomationApp.Droid
{
	
[Activity(Label = "HomeAutomationApp.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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


	private GCMModel gcmClient = null;
	private VoiceCommandController voiceController = null;

	protected override void OnCreate(Bundle savedState)
	{
		base.OnCreate(savedState);

//			api.Interfaces inter = new api.Interfaces(new Uri());
		Bundle bundle = Intent.Extras;

//			if (bundle == null) {
//				bundle = new Bundle ();
//				bundle.PutString ("MODE", "SIM");
//				bundle.PutString ("CONFIG", jsonConfigString);
//				bundle.PutString ("TIMELINE", jsonTimelineString);
//				bundle.PutString ("USER", "user1");
//				bundle.PutString ("PASS", "password");
//			}


		global::Xamarin.Forms.Forms.Init(this, savedState);
		ToastNotificatorImplementation.Init();

		if(bundle == null)
		{
			LoadApplication(new App());
		}
		else
		{
			
			LoadApplication(new App(bundle.Get("MODE").ToString(), 
				bundle.Get("CONFIG").ToString(),
				bundle.Get("TIMELINE").ToString(),  
				bundle.Get("USER").ToString(), 
				bundle.Get("PASS").ToString()));
		}
			
		//Handles setting up GCM Push Notification Service
		gcmClient = new GCMModel(this);
		voiceController = new VoiceCommandController();

	}


	protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
	{
		if(requestCode == 10)
		{
			var textBox = FindViewById<TextView>(Resource.Id.myTextBox);
			if(resultVal == Result.Ok)
			{
				var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
				if(matches.Count != 0)
				{
					textBox.Text = "";
					string textInput = textBox.Text + matches[0];

					// limit the output to 500 characters
					if(textInput.Length > 500)
						textInput = textInput.Substring(0, 500);
					if(textInput.ToLower().Equals("make it brighter near me"))
					{
						textBox.Text = textInput;
						string jsonBlob = null;
						voiceController.makeItBrighterNearMe(jsonBlob);	
					}
					else
						textBox.Text = "No Command Recognized";
				}
			}
		}
		base.OnActivityResult(requestCode, resultVal, data);
	}

	public GCMModel getGCMClient()
	{
		return gcmClient;
	}

	public VoiceCommandController getVoiceController()
	{
		return voiceController;	
	}
}
}

