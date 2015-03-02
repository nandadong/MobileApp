using System;

using Xamarin.Forms;

namespace HomeAutomationApp
{
	public class MainTabbedView : TabbedPage
	{
		public MainTabbedView ()
		{
			Title = "Home Automation";

			Children.Add (new HomeView ());
			Children.Add (new ScenesRootView ());
			Children.Add (new RoomListView ());
			Children.Add (new NotificationView ());
		}
	}
}


