using System;
using System.Collections.Generic;
namespace HomeAutomationApp
{
public class House
{
	private int id;
	private List<Room> roomList;
	public House(int houseID)
	{
		id = houseID;
		roomList = new List<Room>();
	}

public int getID()
{
	return id;
}
public void addRoom(Room r)
{
	roomList.Add(r);
}

public void removeRoom(Room r)
{
	foreach(Room k in roomList)
	{
		if(k.Equals(r))
			roomList.Remove(r);
	}
}

//get all of the rooms currently stored in the device memeory
public List<Room> getRooms()
{
	return roomList;
}

//get a specific room indexed by device id
public Room getRoom(int roomID)
{
	foreach(Room r in roomList)
	{
		if(r.getID().Equals(roomID))
			return r;
	}

	return null;
}
}
}