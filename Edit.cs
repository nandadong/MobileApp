using System;

using Xamarin.Forms;

namespace RoomControl
{
	public class Edit : ContentPage
	{
		public Edit (/*RoomName k*/)
		{
			/*Content = new StackLayout { 
				Children = {
					new Label { Text = "Kitchen" } //this name will change depending on the button pressed
				}
			};*/
			var next = new Button { Text = "Edit" };
			next.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new Add());
			};
			var white = new Label
			{
				Text = /*k.Name*/"Kitchen", //this will change depending on the room clicked
				//BackgroundColor = Color.White,
				XAlign = TextAlignment.Center,
				//Font = Font.SystemFontOfSize (20)
			};
			var listView = new ListView
			{
				RowHeight = 40
			};
			listView.ItemsSource = new RoomName [] {
				new RoomName {Name = "Light #1"},
				new RoomName {Name = "Light #2"}
			};

			listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			var b = new Button { Text = "Edit" };
			b.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new Add());
			};

			//listView.ItemSelected += async (sender, e) => {
				//(ListView(sender)).SelectedItem = a

			//};

			//MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness (20),
					Children = { white, listView, b }
			};
		//}
	}
}

}
