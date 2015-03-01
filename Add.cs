using System;

using Xamarin.Forms;

namespace RoomControl
{
	public class Add : ContentPage
	{
		public Add ()
		{
			var listView = new ListView
			{
				RowHeight = 40
			};
			listView.ItemsSource = new RoomName [] {
				new RoomName {Name = "Light #1"},
				new RoomName {Name = "Light #2"}
			};
			var name = new Label
			{
				Text = "Kitchen",
				//BackgroundColor = Color.White,
				//XAlign = TextAlignment.Center,
				//Font = Font.SystemFontOfSize (20)
			};
			//var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; 
			/*listView.ItemSelected += async (sender, e) => {
				await

			};*/
			Content = new StackLayout { 
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = {
					name, listView
				}
			};
		}
	}
}


