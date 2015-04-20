using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using api;

namespace HomeAutomationApp
{
	public partial class InitialConfigurationView : ContentPage
	{
		
		// Initiate classes and GUI
		public InitialConfigurationView ()
		{
			InitializeComponent ();

			string jsonTimelineString = InitParameters.getInstance ().Timeline;

			// deserialize the JSON for the timeline
			var timeline = JsonConvert.DeserializeObject<SimModel.JsonTimeline> (jsonTimelineString);

			// warning that this is a tab for simulation use only
			var timelineListLabel = new Label {
				Text = "Sim use only: Timeline Information",
				TextColor = Color.Red
			};

			var iface = new Interfaces (new Uri (ConfigModel.Url));
					
			// create list of timeline values
			var timelineListView = new ListView();
			var timelineList = new List<string> {};

			// add timeframe to list
			timelineList.Add ("Timeframe Wall: " + timeline.timeFrame.wall.ToString ());
			timelineList.Add ("Timeframe Sim: " + timeline.timeFrame.sim.ToString ());
			timelineList.Add ("Timeframe Rate: " + timeline.timeFrame.rate.ToString ());


			// add events to list iterating over each event
			foreach (SimModel.JsonEvents item in timeline.events) {
			var value = item.value as HomeAutomationApp.SimModel.JsonGps;	

				timelineList.Add ("Event Time: " + item.time);
				timelineList.Add ("Event Key: " + item.key);
				timelineList.Add ("Event Lat: " + value.lat);
				timelineList.Add ("Event Lon: " + value.lon);
				timelineList.Add ("Event Alt: " + value.altitude);

				var blob = new SimModel.UpdatePositonBlob ();
				blob.lat = value.lat;
				blob.lon = value.lon;
				blob.alt = value.altitude;
				blob.time = item.time;

//				SendPositionAsync (JsonConvert.SerializeObject(blob)).Wait ();

				Debug.WriteLine("HomeAutomationDebug - Position Updated: " + JsonConvert.SerializeObject(blob));
			}

			// set the timeline list to timeline view
			timelineListView.ItemsSource = timelineList;

			// load the components to the layout
			Content = new StackLayout { 
				Padding = new Thickness (20),
				Children = {
					timelineListLabel,
					timelineListView
				}
			};				
		}		

	}



}
	