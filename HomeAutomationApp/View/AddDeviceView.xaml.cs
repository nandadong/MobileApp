using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class AddDeviceView : ContentPage
	{
		public AddDeviceView ()
		{
			InitializeComponent ();

			// Title of tab
			Title = "Devices";

			// Create a device picker 
			var devicePicker = new Picker();

			// get the list of devices
			IList<string> deviceList = new List<string>();
			deviceList.Add("Kitchen Light");
			deviceList.Add("Bedroom Light");
			deviceList.Add("Office Light");

			// Set picker title
			devicePicker.Title = "Select Old Device Name";

			// add the items to the picker
			foreach (var item in deviceList) {
				devicePicker.Items.Add(( item ?? "" ).ToString());
			}
				
			// add content to the view
			Content = new StackLayout {
				Spacing = 20, Padding = 20,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					devicePicker,
					new Entry { Placeholder = "New Device Name" },
					new Entry { Placeholder = "New Room ID" },
					new Button {
						Text = "Update Device",
						TextColor = Color.White,
						BackgroundColor = Color.FromHex ("77D065")
					}
				}
			};
		}
	}
}

