

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using api;

namespace HomeAutomationApp
{
	public partial class AddDeviceView : ContentPage
	{	
		public AddDeviceView ()
		{
			InitializeComponent ();

			// Title of tab
			Title = "Add Device";

			// Create a device picker 
			var devicePicker = new Picker();

			// get the list of devices
			IList<string> deviceList = new List<string>();
			deviceList.Add ("AlarmSystem");
			deviceList.Add ("CeilingFan");
			deviceList.Add ("GarageDoor");
			deviceList.Add ("LightSwitch");
			deviceList.Add ("Thermostat");


			// Set picker title
			devicePicker.Title = "Choose the Device Type";

			// add the items to the picker
			foreach (var item in deviceList) {
				devicePicker.Items.Add(( item ?? "" ).ToString());
			}
		

			// create entries for device name
			var deviceEntry = new Entry();
			deviceEntry.Placeholder = "New Device Name";

			// create button for pushing device settings
			var deviceButton = new Button();
			deviceButton.Text = "Update Device";
			deviceButton.TextColor = Color.White;
			deviceButton.BackgroundColor = Color.FromHex ("77D065");

			// label to display successful push
			var confirmationLabel = new Label ();
			confirmationLabel.Text = "";
			confirmationLabel.TextColor = Color.White;

			// add content to the view
			Content = new StackLayout {
				Spacing = 20, Padding = 20,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					devicePicker,
					deviceEntry,
					deviceButton,
					confirmationLabel
				}
			};
			/*Interfaces deviceInter = new Interfaces(new Uri(ConfigModel.Url));
			List<Device> current_devices = deviceInter.getDevices(2);       // 2 is the house ID

			foreach (Device dev in current_devices)

			{
				if (dev.FullID(RoomID)=0)	
				{
					// register device
				}
			}
			*/
			// handling of button press
			deviceButton.Clicked += (object sender, EventArgs e) => {

				// TODO: check for picker selection
				if ( (deviceEntry.Text) != "" ){
					
					// call the device API to add the user's device
					api.Interfaces deviceInterface = new api.Interfaces(new Uri(ConfigModel.Url));

					//deviceInterface.registerDevice(new Uri(ConfigModel.Url), deviceEntry.Text, deviceList[devicePicker.SelectedIndex], 0);
						
					confirmationLabel.TextColor = Color.Green;
					confirmationLabel.Text = "Success!\nNew device name: " + deviceEntry.Text + "\nNew Device Type: " + deviceList[devicePicker.SelectedIndex] + ".";

				
				} else {
					confirmationLabel.TextColor = Color.Red;
					confirmationLabel.Text = "You must enter a value for device name and room ID.";
				}

			};
				
		}
	}
}
	
