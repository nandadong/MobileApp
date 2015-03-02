using System;
using System.Collections.Generic;

using Xamarin.Forms;

/*
 * The UI components and behavior are seperated.
 * The ScenesView class is a ContentPage class that contains a list of scenes.
 */

namespace HomeAutomationApp
{
	public partial class ScenesRootView : ContentPage
	{
		public ScenesRootView ()
		{
			InitializeComponent ();

			scenesView.ItemsSource = new string []
			{
				"Party", "Evening", "Movie Night", "Sleep"
			};

//			scenesView.ItemSelected += async (sender, e) => {
//				var selected = (string)e.SelectedItem;
//				var scenesWithRooms = new ScenesWithRooms(selected);
//				var next  = new RoomsList(scenesWithRooms);
//				await Navigation.PushAsync(next);
//			};

		}
	}
}

