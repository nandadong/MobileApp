
//using System;
//using System.Collections.Generic;
//using Xamarin.Forms;
//
//namespace HomeAutomationApp
//{
//	public partial class AddDeviceView : ContentPage
//	{
//		public AddDeviceView ()
//		{
//			InitializeComponent ();
//
//			// Title of tab
//			Title = "Devices";
//
//			// Create a device picker 
//			var devicePicker = new Picker();
//
//			// get the list of devices
//			IList<string> deviceList = new List<string>();
//			deviceList.Add("Kitchen Light");
//			deviceList.Add("Bedroom Light");
//			deviceList.Add("Office Light");
//
//			// Set picker title
//			devicePicker.Title = "Select Old Device Name";
//
//			// add the items to the picker
//			foreach (var item in deviceList) {
//				devicePicker.Items.Add(( item ?? "" ).ToString());
//			}
//		
//
//			// create entries for device name and room id
//			var deviceEntry = new Entry();
//			var roomEntry = new Entry ();
//			deviceEntry.Placeholder = "New Device Name";
//			roomEntry.Placeholder = "New Room ID";
//
//			// create button for pushing device settings
//			var deviceButton = new Button();
//			deviceButton.Text = "Update Device";
//			deviceButton.TextColor = Color.White;
//			deviceButton.BackgroundColor = Color.FromHex ("77D065");
//
//			// label to display successful push
//			var confirmationLabel = new Label ();
//			confirmationLabel.Text = "";
//			confirmationLabel.TextColor = Color.White;
//
//			// add content to the view
//			Content = new StackLayout {
//				Spacing = 20, Padding = 20,
//				VerticalOptions = LayoutOptions.Center,
//				Children = {
//					devicePicker,
//					deviceEntry,
//					roomEntry,
//					deviceButton,
//					confirmationLabel
//				}
//			};
//
//			// handling of button press
//			deviceButton.Clicked += (object sender, EventArgs e) => {
//
//				if ( (deviceEntry.Text) != "" && (roomEntry.Text != "") ){
//					confirmationLabel.TextColor = Color.Green;
//					confirmationLabel.Text = "Success!\nNew device name: " + deviceEntry.Text + "\nNew room ID: " + roomEntry.Text + ".";
//
//
//					// user input can be referenced using:
//					// deviceEntry.Text
//					// roomEntry.Text
//
//				
//				} else {
//					confirmationLabel.TextColor = Color.Red;
//					confirmationLabel.Text = "You must enter a value for device name and room ID.";
//				}
//
//			};
//				
//		}
//
//
//		/*
//
//		//please make the spaceID and deviceName user inputs
//		const string blob =  
//		"{" +
//		"		deviceID : 5 ", +
//		"		DeviceName: \"light\","+
//		"		HouseID: 6,"+
//		"		SpaceID: 7,"+
//		"}";
//
//		PostDeviceAsync (JsonConvert.SerializeObject(blob)).Wait ();
//
//		public async Task<object> PostDeviceAsync (string packet) 
//		{
//
//			var client = new HttpClient ();
//			client.Timeout = TimeSpan.FromSeconds (10);
//
//			client.BaseAddress = new Uri(ConfigModel.Url);
//
//			try
//			{
//				var response = await client.PostAsync("api/dev/\\", 
//					new StringContent(packet, Encoding.UTF8, "application/json")).ConfigureAwait(false);
//
//				return response.StatusCode;
//
//			}
//			catch(Exception e)
//			{
//				Debug.WriteLine("HomeAutomationDebugError - Update Error: " + e.Message);
//				Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.InnerException.Message);
//			}
//
//			return null;
//		}
//
//		*/
//	}
//}
//
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace HomeAutomationApp
{
	public partial class AddDeviceView : ContentPage
	{
		public AddDeviceView ()
		{
			InitializeComponent ();

			// Title of tab
			Title = "Devices";

			// Create a device picker 
			var devicePicker = new Picker();

			// get the list of devices
			IList<string> deviceList = new List<string>();
			deviceList.Add("Kitchen Light");
			deviceList.Add("Bedroom Light");
			deviceList.Add("Office Light");

			// Set picker title
			devicePicker.Title = "Select Old Device Name";

			// add the items to the picker
			foreach (var item in deviceList) {
				devicePicker.Items.Add(( item ?? "" ).ToString());
			}
		

			// create entries for device name and room id
			var deviceEntry = new Entry();
			var spaceEntry = new Entry ();
			deviceEntry.Placeholder = "New Device Name";
			spaceEntry.Placeholder = "New Space ID";

			// create button for pushing device settings
			var deviceButton = new Button();
			deviceButton.Text = "Update Device";
			deviceButton.TextColor = Color.White;
			deviceButton.BackgroundColor = Color.FromHex ("77D065");

			// label to display successful push
			var confirmationLabel = new Label ();
			confirmationLabel.Text = "";
			confirmationLabel.TextColor = Color.White;

			// add content to the view
			Content = new StackLayout {
				Spacing = 20, Padding = 20,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					devicePicker,
					deviceEntry,
					spaceEntry,
					deviceButton,
					confirmationLabel
				}
			};

			// handling of button press
			deviceButton.Clicked += (object sender, EventArgs e) => {

				if ( (deviceEntry.Text) != "" && (spaceEntry.Text != "") ){
					confirmationLabel.TextColor = Color.Green;
					confirmationLabel.Text = "Success!\nNew device name: " + deviceEntry.Text + "\nNew room ID: " + spaceEntry.Text + ".";


					// user input can be referenced using:
					// deviceEntry.Text
					// spaceEntry.Text

				
				} else {
					confirmationLabel.TextColor = Color.Red;
					confirmationLabel.Text = "You must enter a value for device name and room ID.";
				}

			};
				
		}



		/*
		//please make the spaceID and deviceName user inputs
		const string blob =  
		"{" +
		"		deviceID : 5 ", +
		"		DeviceName: \"light\","+
		"		HouseID: 6,"+
		"		SpaceID: 7,"+
		"}";

		PostDeviceAsync (JsonConvert.SerializeObject(blob)).Wait ();

		public async Task<object> PostDeviceAsync (string packet) 
		{

			var client = new HttpClient ();
			client.Timeout = TimeSpan.FromSeconds (10);

			client.BaseAddress = new Uri(ConfigModel.Url);

			try
			{
				var response = await client.PostAsync("api/dev/\\", 
					new StringContent(packet, Encoding.UTF8, "application/json")).ConfigureAwait(false);

				return response.StatusCode;

			}
			catch(Exception e)
			{
				Debug.WriteLine("HomeAutomationDebugError - Update Error: " + e.Message);
				Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.InnerException.Message);
			}

			return null;
		}
		*/
	

	}
}
	