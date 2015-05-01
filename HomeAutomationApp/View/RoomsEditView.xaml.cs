using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HomeAutomationApp
{

	public partial class RoomsEditView : ContentPage
	{
		public RoomsEditView ()
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
			listView.ItemTemplate = new DataTemplate (typeof(TweepleCell));
			listView.ItemsSource = new[] { "Light #8", "Light #2", "Light #3" };



			var b = new Button { Text = "Edit" };
			b.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new RoomsAddView());
			};

			//MainPage = new ContentPage {
			Content = new StackLayout {
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = { listView, b }
			};
		}
	}
	public class TweepleCell : ViewCell
	{
		public TweepleCell ()
		{
			var categoryLabel = new Label {
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			categoryLabel.SetBinding (Label.TextProperty, new Binding ("."));

			var onAction = new MenuItem { Text = "On", IsDestructive = false }; 
			onAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			onAction.Clicked += async (sender, e) => {
				var mi = ((MenuItem)sender);
				Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
				//await Navigation.PushAsync (new RoomsAddView());

			};
			ContextActions.Add (onAction);

			var offAction = new MenuItem { Text = "Off", IsDestructive = true }; // red background
			offAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			offAction.Clicked += async (sender, e) => {
				var mi = ((MenuItem)sender);
				Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
			};
			ContextActions.Add (offAction);

			View = new StackLayout {
				Padding = 10,
				Spacing = 0,
				Children = {
					categoryLabel
				}
			};
		}
	}
		public class deleteCell : ViewCell
		{
			public deleteCell ()
			{
				var categoryLabel = new Label {
					VerticalOptions = LayoutOptions.CenterAndExpand
				};

				categoryLabel.SetBinding (Label.TextProperty, new Binding ("."));

				var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
				deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
				deleteAction.Clicked += async (sender, e) => {
					var mi = ((MenuItem)sender);
					Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
				};
				ContextActions.Add (deleteAction);

				View = new StackLayout {
					Padding = 10,
					Spacing = 0,
					Children = {
						categoryLabel
					}
				};
			}
	}
}

