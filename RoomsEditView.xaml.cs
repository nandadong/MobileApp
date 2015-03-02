using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class RoomsEditView : ContentPage
	{
		public RoomsEditView ()
		{
			var next = new Button { Text = "Edit" };
			next.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new RoomsAddView());
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
			listView.ItemsSource = new string [] {
				"Light #1",
				"Light #2"
			};

			//listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			//listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			var b = new Button { Text = "Edit" };
			b.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new RoomsAddView());
			};

			//MainPage = new ContentPage {
			Content = new StackLayout {
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = { white, listView, b }
			};
		}
	}
}

