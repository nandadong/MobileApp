using System;

using Xamarin.Forms;
//using Xamarin.Forms.iNavigation;

namespace RoomControl
{

	public class RoomName {
		public string Name { get; set; }
		public bool Done { get; set; }
		//public string [15];
	}
	public class App : Xamarin.Forms.Application
	{
		public static Page GetMainPage()
		{
			var mainNav = new NavigationPage(new Rooms());
			return mainNav;
		}
		public App ()
		{
			var np = new NavigationPage (new Rooms()) {Title="Room Nav"};

			// The root page of your application
			MainPage = np;

			//listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			//listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");


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

