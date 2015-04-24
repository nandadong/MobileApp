using System;

using Xamarin.Forms;

namespace HomeAutomationApp
{
public class MainTabbedView : TabbedPage
{
	public MainTabbedView()
	{
		Title = "Home Automation";


		// TODO: make if in simulated mode
		if(true)
		{
			Children.Add(new Login());
		}

		Children.Add(new AddDeviceView());
//		Children.Add(new LocationView());
		Children.Add(new HomeView());
		Children.Add(new ScenesRootView());
		Children.Add(new RoomListView());
		Children.Add(new NotificationView());

	}
}
}


