using System;
using System.Collections.Generic;

using Xamarin.Forms;

/*
 * The UI components and behavior are seperated.
 * The RoomsList class is a ContentPage class that contains a list of devices, 
 * which belongs to certain room.
 */

namespace HomeAutomationApp
{
	public partial class DevicesList : ContentPage
	{
		public DevicesList (RoomsWithDevices roomsWithDevices)
		{
			InitializeComponent ();
			devicesList.ItemsSource = roomsWithDevices.Devices;
			Title = roomsWithDevices.Name;

			devicesList.ItemSelected += async (sender, e) => {
				var selected = (string)e.SelectedItem;
				await Navigation.PushAsync(new DevicesPage(selected));
			};
		}
	}
}

