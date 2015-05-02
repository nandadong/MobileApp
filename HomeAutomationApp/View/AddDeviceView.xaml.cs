

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

		// text entry for room
		var roomEntry = new Entry();
		roomEntry.Placeholder = myDeviceModel.roomPlaceholder;


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
			roomEntry.IsEnabled = false;
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
				roomEntry,
				deviceButton,
				confirmationLabel
			}
		};

		// handling of button press
		deviceButton.Clicked += async (object sender, EventArgs e) =>
		{

			if((deviceEntry.Text) != "" && devicePicker.SelectedIndex >= 0)
			{
				// TODO: repair info field
				myDeviceModel.registerDevice(deviceEntry.Text, Convert.ToUInt64(roomEntry.Text), "");						

				// Display a confirmation that the operation was successfu
				await DisplayAlert ("Success!", "Name: " + deviceEntry.Text + "\nDevice: " + myDeviceModel.unregisteredDeviceList[devicePicker.SelectedIndex] + "\nRoom: " + roomEntry.Text,  "OK");

				// Clear the forms after the alert has been displayed
				devicePicker.SelectedIndex = -1;
				deviceEntry.Text = "";
				roomEntry.Text = "";
			}
			else if (deviceEntry.Text == "" && devicePicker.SelectedIndex == -1)
			{
				DisplayAlert ("Error!", "You must enter a value for the device name and select a device.", "OK");
			}
			else if (deviceEntry.Text == "" && devicePicker.SelectedIndex != -1)
			{
				DisplayAlert ("Error!", "You must enter a value for device name.", "OK");
			}
			else if (deviceEntry.Text != "" && devicePicker.SelectedIndex == -1)
			{
				DisplayAlert ("Error!", "You must select a device.", "OK");
			}

		};
				
	}
}
}



