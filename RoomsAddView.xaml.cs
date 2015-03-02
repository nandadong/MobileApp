using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class RoomsAddView : ContentPage
	{
		public RoomsAddView ()
		{
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
			var name = new Label
			{
				Text = "Kitchen"
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

