using System;

/*
 * This class serves as data, specifically data for Scenes list
 */

namespace HomeAutomationApp
{
	public class ScenesWithRooms
	{
		string[] roomList1 = new string[] {"Kitchen", "Hall", "Bedroom"};
		string[] roomList2 = new string[] {"Bedroom#1", "Bedroom#2", "Bedroom#3", "Living Room"};
		string[] roomList3 = new string[] {"Hall", "Living Room", "Bedroom"};
		string[] roomList4 = new string[] {"Bedroom#1", "Bedroom#2", "Living Room", "Kitchen", "Hall"};

		public string Name { get; set; }
		public string[] Rooms { get; set; }

		public ScenesWithRooms (string name)
		{
			Name = name;
			if (name == "Party")
				Rooms = roomList1;
			else if (name == "Evening")
				Rooms = roomList2;
			else if (name == "Movie Night")
				Rooms = roomList3;
			else if (name == "Sleep")
				Rooms = roomList4;
			else
				Rooms = new string[] {"No rooms under this scenes now" };
		}
	}
}

