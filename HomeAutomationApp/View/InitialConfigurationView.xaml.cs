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
			var timeline = JsonConvert.DeserializeObject<UpdateLocationSimModel.JsonTimeline> (jsonTimelineString);

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
			foreach (UpdateLocationSimModel.JsonEvents item in timeline.events) {
				timelineList.Add ("Event Time: " + item.time.ToString());
				timelineList.Add ("Event Key: " + item.key);
				timelineList.Add ("Event Lat: " + item.value.lat.ToString ());
				timelineList.Add ("Event Lon: " + item.value.lon.ToString ());
				timelineList.Add ("Event Alt: " + item.value.alt.ToString ());

				var blob = new UpdateLocationSimModel.UpdatePositonBlob ();
				blob.lat = item.value.lat;
				blob.lon = item.value.lon;
				blob.alt = item.value.alt;
				blob.time = item.time;

				SendPositionAsync (JsonConvert.SerializeObject(blob)).Wait ();

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
			
		public async Task<object> SendPositionAsync (string packet) {

			var client = new HttpClient ();
			client.Timeout = TimeSpan.FromSeconds (10);

			client.BaseAddress = new Uri(ConfigModel.Url);

			try
			{
				var response = await client.PostAsync("api/user/updateposition/user1", 
					new StringContent(packet, Encoding.UTF8, "application/json")).ConfigureAwait(false);

				return response.StatusCode;

			}
			catch(Exception e)
			{
				Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.Message);
				Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.InnerException.Message);
			}

			return null;
		}

	}



}
	