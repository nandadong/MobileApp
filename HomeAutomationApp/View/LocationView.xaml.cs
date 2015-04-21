using System;
using System.Collections.Generic;

using Xamarin.Forms;

using XLabs.Forms.Mvvm;
using Geolocator.Plugin;
using System.Diagnostics;

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
			locator.StartListening(1, 0);

		locator.PositionChanged += async (sender, e) => 
		{
			var position = e.Position;

			Lat.Text = position.Latitude.ToString();
			Lon.Text = position.Longitude.ToString();
			Alt.Text = position.Altitude.ToString();
			Head.Text = position.Heading.ToString();
		};

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
}
}

