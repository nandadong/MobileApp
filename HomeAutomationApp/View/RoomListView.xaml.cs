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

			var white = new Label
			{
				Text = "ROOMS",
				BackgroundColor = Color.White,
				XAlign = TextAlignment.Center,
				//Font = Font.SystemFontOfSize (20)
			};
			var listView = new ListView
			{
				RowHeight = 40
			};
			listView.ItemsSource = new string [] {
				"Kitchen",
				"Hall",
				"Living Room",
				"Bathroom #1",
				"Bedroom #1"
			};

			//listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			//listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			Content = new StackLayout {
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = { white, listView }
			};

			listView.ItemSelected += async (sender, e) => {
				//RoomName k = listView.ItemsSource,
				//await Navigation.PushAsync(new Edit(k)); 
				await Navigation.PushAsync (new RoomsEditView());
			};
		}
	}
}

