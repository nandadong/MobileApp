using System;
using System.Collections.Generic;

using Xamarin.Forms;

/*
 * Login page is start page, replace it with yours.
 * But for the navigation to work properly, use Navigation.PushAsync(new nextPage());
 */


namespace HomeAutomationApp
{
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();

			ClickButton.Clicked += (object sender, EventArgs e) => {
//				mainNav.PushAsync(new ScenesList());
				Navigation.PushAsync(new ScenesView());
			};
		}
	}
}

