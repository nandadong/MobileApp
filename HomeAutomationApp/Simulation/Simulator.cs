using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;

namespace HomeAutomationApp
{
public class Simulator
{
	private readonly SimModel.JsonTimeline TimelineBlob;
	private readonly string User;
	private readonly string Password;

	public Simulator(string config, string timeline, string user, string password)
	{
		TimelineBlob = JsonConvert.DeserializeObject<SimModel.JsonTimeline>(timeline);
		User = user;
		Password = password;
	}

	public void StartSimulation(ObservableCollection<string> items)
	{
		Debug.WriteLine("Simulating " + TimelineBlob.events.ToArray().Length + " Cases");
		int counter = 0;
		int passed = 0;

		var positionController = new UpdatePositionController();

		foreach(SimModel.JsonEvents simEvent in TimelineBlob.events)
		{
			counter++;
			Debug.WriteLine("Scenario " + counter);
			if(simEvent.key.ToLower() == "locationchange")
			{
				
				Debug.WriteLine("Location Change");

				string listValue = "Event Time: " + simEvent.time;
				var value = simEvent.value as SimModel.JsonGps;

				listValue += "Event Lat: " + value.lat;
				listValue += "Event Lon: " + value.lon;
				listValue += "Event Alt: " + value.altitude;

				items += listValue;

				var blob = new SimModel.UpdatePositonBlob();
				blob.lat = value.lat;
				blob.lon = value.lon;
				blob.alt = value.altitude;
				blob.time = simEvent.time;

				var retStatus = positionController.SendPositionAsync(blob.ToString(), User);
				if(retStatus != HttpStatusCode.OK)
				{
					passed++;
					Debug.WriteLine("Success");
				}
				else
				{
					Debug.WriteLine("Failed. Expected OK. Received " + retStatus);
				}

			}
			else
			if(simEvent.key.ToLower() == "physicalchange")
			{
				
				Debug.WriteLine("Physical Change");

				string listValue = "Event Time: " + simEvent.time;
				var value = simEvent.value as SimModel.JsonGps;

				listValue += "Event Lat: " + value.lat;
				listValue += "Event Lon: " + value.lon;
				listValue += "Event Alt: " + value.altitude;

				items += listValue;

				var blob = new SimModel.UpdatePositonBlob();
				blob.lat = value.lat;
				blob.lon = value.lon;
				blob.alt = value.altitude;
				blob.time = simEvent.time;

				var retStatus = positionController.SendPositionAsync(blob.ToString(), User);
				if(retStatus != HttpStatusCode.OK)
				{
					passed++;
					Debug.WriteLine("Success");
				}
				else
				{
					Debug.WriteLine("Failed. Expected OK. Received " + retStatus);
				}

			}
				
		}
	}
		

}
}

