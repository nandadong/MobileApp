using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class InitialConfigurationView : ContentPage
	{

		// classes for timeline blob
		public class JsonGps
		{
			public double lat { get; set; }
			public double lon { get; set; }
			public double alt { get; set; }
		}
			
		public class JsonEvents
		{
			public string time { get; set; }
			public string key { get; set; }
			public JsonGps value { get; set; }
		}

		public class JsonTimeline
		{
			public int timeFactor { get; set; }
			public List<JsonEvents> events { get; set; }
		}


		// Initiate classes and GUI
		public InitialConfigurationView ()
		{
			InitializeComponent ();

			// JSON timeline information
			const string jsonTimelineString =
			"{" +
			"	timeFactor : 200," +
			"	events : [" +
			"		{" +
			"			time : \"simulated second - logical timestamp\"," +
			"			key : \"locationChange\"," +
				"		value : {" +
			"				lat : 1.123456," +
			"				lon : 2.123456," +
			"				alt : 3.123456" +
				"		}" +
			"		}" +
			"	]" +
			"}";

			// deserialize the JSON for the timeline
			var timeline = JsonConvert.DeserializeObject<JsonTimeline> (jsonTimelineString);

			// warning that this is a tab for simulation use only
			var timelineListLabel = new Label {
				Text = "Sim use only: Timeline Information",
				TextColor = Color.Red
			};
					
			// create list of timeline values
			var timelineListView = new ListView();
			var timelineList = new List<string> {};

			// add timefactor to list
			timelineList.Add ( "Time Factor: " + timeline.timeFactor.ToString() );
		
			// add events to list iterating over each event
			foreach (JsonEvents item in timeline.events) {
				timelineList.Add ("Event Time: " + item.time);
				timelineList.Add ("Event Key: " + item.key);
				timelineList.Add ("Event Lat: " + item.value.lat.ToString ());
				timelineList.Add ("Event Lon: " + item.value.lon.ToString ());
				timelineList.Add ("Event Alt: " + item.value.alt.ToString ());
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

