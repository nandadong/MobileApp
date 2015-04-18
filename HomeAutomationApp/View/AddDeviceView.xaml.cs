

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using api;

namespace HomeAutomationApp
{
public partial class AddDeviceView : ContentPage
{
	public AddDeviceView()
	{
		InitializeComponent();

		// instantiate device model to access values
		AddDeviceModel myDeviceModel = new AddDeviceModel();

		// tab title
		Title = myDeviceModel.tabTitle;

		// label for debugging
		var debugLabel = new Label();
		debugLabel.Text = myDeviceModel.debugLabel;

		// main label
		var registerLabel = new Label();
		registerLabel.Text = myDeviceModel.registerLabel;
		registerLabel.TextColor = Color.White;

		// device picker for unregistered devices
		var devicePicker = new Picker();
		if(myDeviceModel.unregisteredDeviceList == null)
		{
			devicePicker.Title = "No Unregistered Devices Found";
		}
		else
		{
			devicePicker.Title = "Select an Unregistered Device";
			foreach(string device in myDeviceModel.unregisteredDeviceList)
			{
				devicePicker.Items.Add((device ?? "").ToString());
			}
		}
			

		// text entry for device name
		var deviceEntry = new Entry();
		deviceEntry.Placeholder = myDeviceModel.namePlaceholder;

		// button for pushing device settings
		var deviceButton = new Button();
		deviceButton.Text = myDeviceModel.buttonText;
		deviceButton.TextColor = Color.White;
		deviceButton.BackgroundColor = Color.FromHex("77D065");


		// label to display successful push
		var confirmationLabel = new Label();
		confirmationLabel.Text = "";
		confirmationLabel.TextColor = Color.White;


		// disable forms if device list is empty
		if(myDeviceModel.isDeviceListEmpty == true)
		{
			registerLabel.Text = "No devices need to be registered.";
			registerLabel.TextColor = Color.Green;
			devicePicker.IsEnabled = false;
			deviceEntry.IsEnabled = false;
			deviceButton.IsEnabled = false;
		}


		// add content to the view
		Content = new StackLayout {
			Spacing = 20, Padding = 20,
			VerticalOptions = LayoutOptions.Center,
			Children = {
				registerLabel,
				devicePicker,
				deviceEntry,
				deviceButton,
				confirmationLabel
			}
		};

		// handling of button press
		deviceButton.Clicked += (object sender, EventArgs e) =>
		{
			
			if((deviceEntry.Text) != "")
			{
				// TODO: repair info field
				myDeviceModel.registerDevice(deviceEntry.Text, "");						
				confirmationLabel.TextColor = Color.Green;
				confirmationLabel.Text = "Success!\nNew device name: " + deviceEntry.Text + "\nNew Device Type: " + /*deviceList[devicePicker.SelectedIndex] +*/ ".";
			}
			else
			{
				confirmationLabel.TextColor = Color.Red;
				confirmationLabel.Text = "You must enter a value for device name and room ID.";
			}

		};
				
	}
}
}



