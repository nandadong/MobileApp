using System;
using System.Collections.Generic;
using api;
namespace HomeAutomationApp
{
public static class House
{
	private static int id;
	private static List<Room> roomList;
	/*public static House(int houseID)
	{
		id = houseID;
		roomList = new List<Room>();
	}*/


	public static void createHouse(int houseID)
	{
		id = houseID;
		roomList = new List<Room>();
	}

	public static bool inHouse(Device dev){
		foreach(Room r in roomList)
		{
			foreach(Device d in r.getDevices()){
				if(d.ID.DeviceID == dev.ID.DeviceID)
					return true;
			}
		}
		return false;
	}

	public static bool inRoom(Device dev, Room r){
		foreach(Device d in r.getDevices())
		{
			if(d.ID.DeviceID == dev.ID.DeviceID)
				return true;
		}

		return false;
	}
	public static void updateHouse(List<Device> deviceList){
		foreach(Device d in deviceList)
		{
			if(inHouse(d))
			{
				foreach(Room r in roomList)
				{
					if(inRoom(d, r))
						r.updateDevice(d);
						
				}
			}
			else
				addDevice(d, (int)d.ID.RoomID);
		}
			
	}
	public static int getID()
	{
		return id;
	}
	public static void addRoom(int roomID)
	{
		Room toAdd = new Room(roomID);
		if(!roomExists(roomID))
			roomList.Add(new Room(roomID));
	}

	public static void addRoom(Room r)
	{
		if(!roomExists(r.getID()))
			roomList.Add(r);
	}

	public static bool roomExists(int roomId)
	{
		foreach(Room r in roomList)
		{
			if(r.getID() == roomId)
				return true;
		}

		return false;
	}

	public static void addDevice(Device d, int roomId){
		if(roomExists(roomId))
		{
			getRoom(roomId).addDevice(d);
		}

		else
		{
			addRoom(roomId);
			getRoom(roomId).addDevice(d);
		}
			
	}

	public static void removeRoom(Room r)
	{
		foreach(Room k in roomList)
		{
			if(k.Equals(r))
				roomList.Remove(r);
		}
	}

//get all of the rooms currently stored in the device memeory
	public static List<Room> getRooms()
	{
		return roomList;
	}

//get a specific room indexed by device id
	public static Room getRoom(int roomID)
	{
		foreach(Room r in roomList)
		{
			if(r.getID() == roomID)
				return r;
		}

	return null;
}
}
}