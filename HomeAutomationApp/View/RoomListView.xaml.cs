using System;
using System.Collections.Generic;
using api;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HomeAutomationApp
{

	public partial class RoomListView : ContentPage
	{
		public RoomListView ()
		{

			Title = "Rooms";

			InvalidationHelper help = new InvalidationHelper();
			AffectedDevices ad = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[0]);
			AffectedDevices ad2 = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[1]);
			AffectedDevices ad3 = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[2]);
			List<api.Device> devices = new List<api.Device>();

			IDeviceInput k = new HouseInput("","");
			IDeviceOutput u = new HouseOutput("","");
			Hats.Time.TimeFrame t = new Hats.Time.TimeFrame();

			GarageDoor gd = new GarageDoor(k, u, t);
			gd.Enabled = ad.Enabled;
			gd.ID.RoomID = Convert.ToUInt64(ad.ID["roomID"]);
			gd.ID.DeviceID = Convert.ToUInt64(ad.ID["deviceID"]);
			gd.Enabled = ad.Enabled;
			devices.Add(gd);

			LightSwitch li = new LightSwitch(k, u, t);
			li.Enabled = ad.Enabled;
			li.ID.RoomID = Convert.ToUInt64(ad2.ID["roomID"]);
			li.ID.DeviceID = Convert.ToUInt64(ad2.ID["deviceID"]);
			devices.Add(li);

		House.createHouse(Convert.ToInt32(ad.ID["houseID"]));
		House.updateHouse(devices);
//			var white = new Label
//			{
//				Text = "ROOMS",
//				BackgroundColor = Color.White,
//				XAlign = TextAlignment.Center,
//				//Font = Font.SystemFontOfSize (20)
//			};

			var listView = new ListView();
			/*listView.ItemsSource = new string [] {
				"Kitchen",
				"Hall",
				"Bedroom",
				"Bathroom",
				"Living Room"
			};*/

		listView.ItemsSource = House.roomListToString();

			Content = new StackLayout {
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				//Children = { listView }
			Children = {listView}
			};

			listView.ItemSelected += async (sender, e) => {
				await Navigation.PushAsync (new RoomsEditView());
			};
		}
	}
}

