using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class AlertsView : ContentPage
	{
		public AlertsView ()
		{
			InitializeComponent ();

			Title = "Alerts";

			AlertsList.ItemsSource = new string [] {
				"Kitchen lights ON",
				"Main Door unlocked"
			};
		}
	}
}

