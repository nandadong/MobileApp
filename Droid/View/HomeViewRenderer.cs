using System;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Speech;
using Android.Util;

[assembly:ExportRenderer(typeof(HomeAutomationApp.HomeView), typeof(HomeAutomationApp.Droid.HomeViewRenderer))]

namespace HomeAutomationApp.Droid
{
	public class HomeViewRenderer : PageRenderer
	{
		Android.Views.View view;
		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			// this is a ViewGroup - so should be able to load an AXML file and FindView<>
			var activity = this.Context as Activity;

			//This line needs to be fixed. How do we Add A HomeLayout to the resource.designer.cs
			var o = activity.LayoutInflater.Inflate(Resource.Layout.HomeLayout, this, false);
			view = o;

			var button = view.FindViewById<Android.Widget.ImageButton> (Resource.Id.micButton);
			var textBox = FindViewById<TextView> (Resource.Id.myTextBox);
			button.Click += (object sender, EventArgs btnevent) => {
				// create the intent and start the activity
				var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
				voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

				// put a message on the modal dialog
				//voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));

				// if there is more then 1.5s of silence, consider the speech over
				voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
				voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
				voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
				voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

				// you can specify other languages recognised here, for example
				// voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
				// if you wish it to recognise the default Locale language and German
				// if you do use another locale, regional dialects may not be recognised very well

				voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
				activity.StartActivityForResult(voiceIntent, 10);
			};

			AddView(view);

		}



		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);
			var msw = MeasureSpec.MakeMeasureSpec (r - l, MeasureSpecMode.Exactly);
			var msh = MeasureSpec.MakeMeasureSpec (b - t, MeasureSpecMode.Exactly);
			view.Measure(msw, msh);
			view.Layout (0, 0, r - l, b - t);
		}
	}
}

