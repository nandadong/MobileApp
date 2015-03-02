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
			Children.Add (new ScenesView ());
			Children.Add (new NotificationView ());
		}
	}
}


