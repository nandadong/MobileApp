using System;
using System.Collections.Generic;

using Xamarin.Forms;

/*
 * The UI components and behavior are seperated.
 * The RoomsList class is a ContentPage class that contains a list of rooms, 
 * which belongs to specific scenes.
 * Note that the content of Scenes is debatable, the section is subject to change.
 */

namespace HomeAutomationApp
{
	public partial class RoomsList : ContentPage
	{
		public RoomsList (ScenesWithRooms scene)
		{
			InitializeComponent ();
			scenesList.ItemsSource = scene.Rooms;
			Title = scene.Name;

			scenesList.ItemSelected += async (sender, e) => {
				var selected = (string)e.SelectedItem;
				var roomsWithDevices = new RoomsWithDevices(selected);
				var next  = new DevicesList(roomsWithDevices);
				await Navigation.PushAsync(next);
			};
		}
	}
}

