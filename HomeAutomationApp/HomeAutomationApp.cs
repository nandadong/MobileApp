using System;

using Xamarin.Forms;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeAutomationApp
{
	public class App : Application
	{
		public App (string mode, string config, string timeline, string user, string password)
		{
			// The root page of your application
//			MainPage = new MainTabbedView ();

			InitParameters.setInstance (mode, config, timeline, user, password);

			var configObj = JsonConvert.DeserializeObject<JObject> (config);
			ConfigModel.Url = configObj.GetValue ("serverLocation").ToString();

			MainPage = new NavigationPage(new MainTabbedView());

			Debug.WriteLine ("Mode is: " + mode);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

