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
				HeightRequest = 100,
				WidthRequest = 100,
			};

			LayoutStack.Children.Add (b);
		}
	}
}

