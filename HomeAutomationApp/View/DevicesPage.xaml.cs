using System;
using System.Collections.Generic;

using Xamarin.Forms;

/*
 * This class is a simple demo of page that display devices information.
 * And possiblities to control the devices.
 * More elegent ways of interaction with devieces will be implemented in the future
 */

namespace HomeAutomationApp
{
	public partial class DevicesPage : ContentPage
	{
		public DevicesPage (string name)
		{
			InitializeComponent ();
			deviceName.Text = name;
		}
	}
}

