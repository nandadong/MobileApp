using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class HomeView : ContentPage
	{

		public HomeView ()
		{
			InitializeComponent ();

			Title = "Home";

			var b = new Button () {
				Image = "Mic.png",
				HeightRequest = 200,
				WidthRequest = 150,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};

			if (Device.OS == TargetPlatform.Android) {
				b.BackgroundColor = Color.Gray;
			}

			AlertsBtn.BackgroundColor = Color.FromHex ("D9534F");
			AlertsBtn.TextColor = Color.White;

			AlertsBtn.Clicked += (object sender, EventArgs e) => {
				Navigation.PushAsync(new AlertsView());
			};

			LayoutGrid.Children.Add (b);
		}

	}
}

