using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class RoomsAddView : ContentPage
	{
		public RoomsAddView ()
		{

			Title = "Kitchen";
//			var white = new Label
//			{
//				Text = /*k.Name*/"Kitchen", //this will change depending on the room clicked
//				//BackgroundColor = Color.White,
//				XAlign = TextAlignment.Center,
//				//Font = Font.SystemFontOfSize (20)
//			};

			var listView = new ListView ();
			listView.ItemTemplate = new DataTemplate (typeof(deleteCell));
			listView.ItemsSource = new[] { "Light #1", "Light #2", "Light #3" };

			var b = new Button { Text = "Done" };
			b.Clicked += async (sender, e) => {
				//await Navigation.PushAsync (new RoomListView());
			};
			Content = new StackLayout { 
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = {
					listView
				}
			};
		}
	}
}

