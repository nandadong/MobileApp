using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HomeAutomationApp
{
	public partial class InitialConfigurationView : ContentPage
	{

		public class JSONCoordinates
		{
			public string x { get; set; }
			public string y { get; set; }
			public string z { get; set; }

		}

		public class JSONUsers
		{
			public string Username { get; set; }
			public int UserID { get; set; }
			public string Password { get; set; }
			public JSONCoordinates coordinates { get; set; }
		}

		public class JSONConfiguration
		{
			public string storageLocation { get; set; }
			public List<JSONUsers> users { get; set; }
		}


		public InitialConfigurationView ()
		{
			InitializeComponent ();

			const string json = 
				"{" +
				"	\"storageLocation\" : \"123.45.67.890\"," +
				"	\"users\" : [{ \"Username\":\"John\", \"UserID\":369, \"Password\":\"MyPass\", \"Coordinates\": { \"x\":\"1.123456\", \"y\":\"2.123456\", \"z\":\"3.123456\"}}," +
				"		         { \"Username\":\"Jacob\",\"UserID\":123, \"Password\":\"HisPass\", \"Coordinates\": { \"x\":\"4.123456\", \"y\":\"5.123456\", \"z\":\"6.123456\"}" +
				"   }]" +
				"}";

			// Deserialize the JSON placing information in their associated class
			var myConfig = JsonConvert.DeserializeObject<JSONConfiguration> (json);

	
			// warning that this is a tab for simulation use only
			var myLabel = new Label {
				Text = "! This tab is for simulation use only !",
				TextColor = Color.Red
			};


			// create a list to display the configuration information
			var listView = new ListView ();
			var myList = new List<string> {
				"Storage location: " + myConfig.storageLocation
			};


			// append the config values to the list
			foreach (JSONUsers item in myConfig.users) {
				myList.Add ("Username: " + item.Username);
				myList.Add ("User ID: " + item.UserID.ToString ());
				myList.Add ("User Password: " + item.Password);
				myList.Add ("User Location: Lat " + item.coordinates.x + ", Lon " + item.coordinates.y + ", Alt " + item.coordinates.z);


			}

			// set the list of config values to that will be displayed
			listView.ItemsSource = myList;

			// load the components to the layout
			Content = new StackLayout { 
				Padding = new Thickness (20),
				Children = {
					myLabel,
					listView
				}
			};
					
		}
			
	}
}

