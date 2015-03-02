using System;

/*
 * This class serves as data, specifically data for rooms list
 */


namespace HomeAutomationApp
{
	public class RoomsWithDevices
	{
		string[] kitchen = {"Doors", "Windows", "Thermostat", "Light#1", "Light#2" };
		string[] livingRoom = {"TV", "Air Conditoner", "Thermostat", "Camera", "Furnace", "Light#1", "Light#2" };
		string[] bedroom = {"Doors", "Windows", "TV", "Curtain", "Light#1", "Light#2", "Light#3" };
		string[] Hall = {"Light#1", "Light#2", "Doors", "Windows", "Camera" };

		public string Name { get; set; }
		public string[] Devices { get; set; }

		public RoomsWithDevices (string name)
		{
			Name = name;
			if (name == "Kitchen")
				Devices = kitchen;
			else if (name == "Hall")
				Devices = Hall;
			else if (name == "Living Room")
				Devices = livingRoom;
			else if (name.Contains ("Bedroom"))
				Devices = bedroom;
			else
				Devices = new string[] { "No devices connected yet" };
		}
	}
}

