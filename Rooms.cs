using System;

using Xamarin.Forms;

namespace RoomControl
{
	public class Rooms : ContentPage
	{

		public Rooms ()
		{
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
			listView.ItemsSource = new RoomName [] {
				new RoomName {Name = "Kitchen"},
				new RoomName {Name = "Hall"},
				new RoomName {Name = "Living Room"},
				new RoomName {Name = "Bathroom #1"},
				new RoomName {Name = "Bedroom #1"}
			};

			listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			Content = new StackLayout {
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = { white, listView }
			};

			listView.ItemSelected += async (sender, e) => {
				//RoomName k = listView.ItemsSource,
				//await Navigation.PushAsync(new Edit(k)); 
				await Navigation.PushAsync (new Edit());
			};
		}
	}
}


