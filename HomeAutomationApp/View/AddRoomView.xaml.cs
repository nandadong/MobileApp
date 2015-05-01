using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using api;
using Geolocator.Plugin;
using XLabs.Forms.Mvvm;
using Geolocator.Plugin;
using System.Diagnostics;
using Geolocator.Plugin.Abstractions;
using Toasts.Forms.Plugin.Abstractions;
using Newtonsoft.Json;

namespace HomeAutomationApp
{
public partial class AddRoomView : ContentPage
{
	public AddRoomView()
	{
		InitializeComponent();

		// instantiate device model to access values
		AddRoomModel myRoomModel = new AddRoomModel();

		// tab title
		Title = myRoomModel.tabTitle;

		// label for debugging
		var debugLabel = new Label();
		debugLabel.Text = myRoomModel.debugLabel;

		// main label
		var registerLabel = new Label();
		registerLabel.Text = myRoomModel.registerLabel;
		registerLabel.TextColor = Color.White;
				
	
		// text entry for device name
		var nameEntry = new Entry();
		nameEntry.Placeholder = myRoomModel.namePlaceholder;

		var latLabel = new Label();
		latLabel.Text = "Latitude";

		var longLabel = new Label();
		longLabel.Text = "Longitude";

		var altLabel = new Label();
		altLabel.Text = "Altitude";

		// button for pushing device settings
		var addButton = new Button();
		addButton.Text = myRoomModel.buttonText;
		addButton.TextColor = Color.White;
		addButton.BackgroundColor = Color.FromHex("77D065");

		var getButton = new Button();
		getButton.Text = "Get Coordinates";
		getButton.TextColor = Color.White;
		getButton.BackgroundColor = Color.FromHex("77D065");




		// label to display successful push
		var confirmationLabel = new Label();
		confirmationLabel.Text = "";
		confirmationLabel.TextColor = Color.White;


		// add content to the view
		Content = new StackLayout {
			Spacing = 20, Padding = 20,
			VerticalOptions = LayoutOptions.Center,
			Children = {
				registerLabel,
				nameEntry,
				latLabel,
				longLabel,
				altLabel,
				getButton,
				addButton,
				confirmationLabel
			}
		};


		var locator = CrossGeolocator.Current;
		locator.DesiredAccuracy = 50;

		if(!locator.IsListening)
		{
			locator.StopListening();
			locator.StartListening(5, 1);

		}


		getButton.Clicked += async (sender, e) =>
		{
			try
			{
				var position = await locator.GetPositionAsync(timeout: 10000);

				latLabel.Text = position.Latitude.ToString();
				longLabel.Text = position.Longitude.ToString();
				altLabel.Text = position.Altitude.ToString();
			}
			catch(Exception ex)
			{

			}
		};

		// handling of button press
		addButton.Clicked += (object sender, EventArgs e) =>
		{
			
//			if((nameEntry.Text) != "")
//			{
//				// TODO: repair info field
//				myRoomModel.registerDevice(nameEntry.Text, "");						
//				confirmationLabel.TextColor = Color.Green;
//				confirmationLabel.Text = "Success!\nYou have assigned the name: " + nameEntry.Text + "\nTo the device: " + myRoomModel.unregisteredDeviceList[devicePicker.SelectedIndex];
//			
//
//			}
//			else
//			{
//				confirmationLabel.TextColor = Color.Red;
//				confirmationLabel.Text = "You must enter a value for device name and room ID.";
//			}

		};


				
	}
}
}



