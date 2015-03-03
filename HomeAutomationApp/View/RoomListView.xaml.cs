using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HomeAutomationApp
{

	public partial class RoomListView : ContentPage
	{
		public RoomListView ()
		{

			Title = "Rooms";

//			var white = new Label
//			{
//				Text = "ROOMS",
//				BackgroundColor = Color.White,
//				XAlign = TextAlignment.Center,
//				//Font = Font.SystemFontOfSize (20)
//			};

			var listView = new ListView();
			listView.ItemsSource = new string [] {
				"Kitchen",
				"Hall",
				"Bedroom",
				"Bathroom",
				"Living Room"
			};

			Content = new StackLayout {
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = { listView }
			};

			listView.ItemSelected += async (sender, e) => {
				await Navigation.PushAsync (new RoomsEditView());
			};
		}
	}
}

