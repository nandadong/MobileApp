using System;

using Xamarin.Forms;
using System.Collections;
using System.Diagnostics;

namespace HomeAutomationApp
{
	public class App : Application
	{
		public App (string mode, string config, string timeline, string user, string password)
		{
			// The root page of your application
//			MainPage = new MainTabbedView ();
			MainPage = new NavigationPage(new Login());

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

