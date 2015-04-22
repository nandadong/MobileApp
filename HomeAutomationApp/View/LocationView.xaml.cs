using System;
using System.Collections.Generic;

using Xamarin.Forms;

using XLabs.Forms.Mvvm;
using Geolocator.Plugin;
using System.Diagnostics;
using Geolocator.Plugin.Abstractions;
using Toasts.Forms.Plugin.Abstractions;
using Newtonsoft.Json;

namespace HomeAutomationApp
{
public partial class LocationView : ContentPage
{
	public LocationView()
	{
		InitializeComponent();

		Title = "Location";

		var locator = CrossGeolocator.Current;
		locator.DesiredAccuracy = 50;

		if(!locator.IsListening)
		{
			locator.StopListening();
			locator.StartListening(5, 1);

		}

		locator.PositionChanged += onLocation;

		UpdateLocation.Clicked += async (sender, e) =>
		{
			try
			{
				var position = await locator.GetPositionAsync(timeout: 10000);

				Lat.Text = position.Latitude.ToString();
				Lon.Text = position.Longitude.ToString();
				Alt.Text = position.Altitude.ToString();
				Head.Text = position.Heading.ToString();
			}
			catch(Exception ex)
			{

			}
		};

	}

	public async void onLocation(object sender, PositionEventArgs e)
	{
		var position = e.Position;

		Lat.Text = position.Latitude.ToString();
		Lon.Text = position.Longitude.ToString();
		Alt.Text = position.Altitude.ToString();
		Head.Text = position.Heading.ToString();

		var packet = new SimModel.UpdatePositonBlob();
		packet.alt = position.Altitude;
		packet.lat = position.Latitude;
		packet.lon = position.Longitude;
		packet.userID = "user1";
		packet.time = DateTime.Now;

		var str = JsonConvert.SerializeObject(packet);
		var result = new UpdatePositionController().SendPositionAsync(str, "user1");

		var notificator = DependencyService.Get<IToastNotificator>();
		bool tapped = await notificator.Notify(ToastNotificationType.Info, 
			"Position Update", "Result " + result, TimeSpan.FromSeconds(2));

	}
}
}

