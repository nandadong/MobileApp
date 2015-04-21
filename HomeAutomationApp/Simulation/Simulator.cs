using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using api;

namespace HomeAutomationApp
{
public class Simulator
{
	private readonly SimModel.JsonTimeline TimelineBlob;
	private readonly string User;
	private readonly string Password;

	public Simulator(string config, string timeline, string user, string password)
	{
		var tBlob = JsonConvert.DeserializeObject<JObject>(timeline);
		TimelineBlob = new SimModel.JsonTimeline();
		TimelineBlob.timeFrame = tBlob.GetValue("timeFrame").ToObject<SimModel.JsonTimeFrame>();
		TimelineBlob.events = new List<SimModel.JsonEvents>();
		var eBlobs = tBlob.GetValue("events");
		foreach(JObject eBlob in eBlobs)
		{
			var eJson = new SimModel.JsonEvents();
			eJson.key = eBlob.GetValue("key").ToString();
			eJson.time = eBlob.GetValue("time").ToObject<DateTime>();

			switch(eJson.key.ToLower())
			{
			case "locationchange":
				eJson.value = JsonConvert.DeserializeObject<SimModel.JsonGps>(eBlob.GetValue("value").ToString());
				break;
			case "physicalchange":
				eJson.value = JsonConvert.DeserializeObject<SimModel.JsonPhysicalChange>(eBlob.GetValue("value").ToString());
				break;
			case "voicechange":
				eJson.value = JsonConvert.DeserializeObject<SimModel.JsonVoiceChange>(eBlob.GetValue("value").ToString());
				break;
			case "adddevice":
				eJson.value = JsonConvert.DeserializeObject<SimModel.JsonAddDevice>(eBlob.GetValue("value").ToString());
				break;
			}

			TimelineBlob.events.Add(eJson);

		}

		User = user;
		Password = password;
	}

	public void StartSimulation(ObservableCollection<string> items)
	{
		Debug.WriteLine("HAD: Simulating " + TimelineBlob.events.ToArray().Length + " Cases");
		int counter = 0;
		int passed = 0;

		var positionController = new UpdatePositionController();

		foreach(SimModel.JsonEvents simEvent in TimelineBlob.events)
		{
			counter++;
			Debug.WriteLine("Scenario " + counter);
			if(simEvent.key.ToLower() == "locationchange")
			{
				
				Debug.WriteLine("HAD: Location Change");

				string listValue = "Event Time: " + simEvent.time;
				var value = simEvent.value as SimModel.JsonGps;

				listValue += " Event Lat: " + value.lat;
				listValue += " Event Lon: " + value.lon;
				listValue += " Event Alt: " + value.altitude;

				items.Add(listValue);
				Debug.WriteLine("HAD: " + listValue);

				var blob = new SimModel.UpdatePositonBlob();
				blob.lat = value.lat;
				blob.lon = value.lon;
				blob.alt = value.altitude;
				blob.time = simEvent.time;

				var retStatus = positionController.SendPositionAsync(blob.ToString(), User);
				if(retStatus.Result == HttpStatusCode.OK)
				{
					passed++;
					Debug.WriteLine("HAD: Success");
				}
				else
				{
					Debug.WriteLine("HAD: Failed. Expected OK. Received " + retStatus);
				}

			}
			else
			if(simEvent.key.ToLower() == "physicalchange")
			{
				
				Debug.WriteLine("HAD: Physical Change");

				string listValue = "Event Time: " + simEvent.time;
				var value = simEvent.value as SimModel.JsonPhysicalChange;

				listValue += " Device ID: " + value.deviceid;
				listValue += " Device Type: " + value.type;
				listValue += " Device Value: " + value.value;

				items.Add(listValue);
				Debug.WriteLine("HAD: " + listValue);
				Debug.WriteLine("HAD: Updating Device State");			

				passed++;
				Debug.WriteLine("HAD: Success");

			}
			else
			if(simEvent.key.ToLower() == "voicechange")
			{

				Debug.WriteLine("HAD: Voice Command");

				string listValue = "Event Time: " + simEvent.time;
				var value = simEvent.value as SimModel.JsonVoiceChange;

				listValue += " Action: " + value.action;
				listValue += " Event Lat: " + value.lat;
				listValue += " Event Lon: " + value.lon;
				listValue += " Event Alt: " + value.altitude;

				items.Add(listValue);
				Debug.WriteLine("HAD: " + listValue);
				Debug.WriteLine("HAD: Sending Voice Command");			

				passed++;
				Debug.WriteLine("HAD: Success");

			}
			else
			if(simEvent.key.ToLower() == "adddevice")
			{

				Debug.WriteLine("HAD: Add Device");

				string listValue = "Event Time: " + simEvent.time;
				var value = simEvent.value as SimModel.JsonAddDevice;

				listValue += " Device Name: " + value.name;
				listValue += " Device Type: " + value.type;
				listValue += " Room Id: " + value.roomid;

				items.Add(listValue);
				Debug.WriteLine("HAD: " + listValue);
				Debug.WriteLine("HAD: Adding Device");			

				passed++;
				Debug.WriteLine("HAD: Success");

			}
				
		}
		Debug.WriteLine("HAD: Tests Passed " + passed + "/" + counter);
	}
		

}
}

