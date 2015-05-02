using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using api;
using Geolocator.Plugin;
using XLabs.Forms.Mvvm;
using System.Diagnostics;
using Geolocator.Plugin.Abstractions;
using Toasts.Forms.Plugin.Abstractions;
using System.Net.Http;

namespace HomeAutomationApp
{
public partial class AddRoomView : ContentPage
{
	public AddRoomView()
	{
		InitializeComponent();


		Title = "Room";

		// label for debugging
		var debugLabel = new Label();

		// main label
		var registerLabel = new Label();
		registerLabel.TextColor = Color.White;


		// text entry for device name
		var nameEntry = new Entry();

		bool locationSet = false;

		var latLabel = new Label();
		latLabel.Text = "Latitude";

		var longLabel = new Label();
		longLabel.Text = "Longitude";

		var altLabel = new Label();
		altLabel.Text = "Altitude";

		// button for pushing device settings
		var addButton = new Button();
		addButton.Text = "Register Room";
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
			var position = await locator.GetPositionAsync(timeout: 10000);

			latLabel.Text = position.Latitude.ToString();
			longLabel.Text = position.Longitude.ToString();
			altLabel.Text = position.Altitude.ToString();
			locationSet = true;
		};

		// handling of button press
		addButton.Clicked += (object sender, EventArgs e) =>
		{

			if((nameEntry.Text) != "" && locationSet == true)
			{
				// TODO: repair info field

				JObject blob = new JObject();
				blob["HouseID"] = 2;
				blob["name"] = nameEntry.Text;
				blob["lat"] = latLabel.Text;
				blob["long"] = longLabel.Text;
				blob["alt"] = altLabel.Text;

				HttpResponseMessage response = AddRoomController.SendRoomAsync(blob.ToString());						
				confirmationLabel.TextColor = Color.Green;
				confirmationLabel.Text = "Success!\nYou have registered a room with the name: " + nameEntry.Text + "\nServer Response: " + response.ToString();
			}
			else
			{
				confirmationLabel.TextColor = Color.Red;
				confirmationLabel.Text = "You must enter a value for room name and location.";
			}

		};



	}
}
}



