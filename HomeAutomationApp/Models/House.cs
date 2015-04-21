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

	public static void updateHouse(List<Device> deviceList){
		foreach(Device d in deviceList)
		{
			foreach(Room r in roomList)
			{
				if(!r.getDevices().Contains(d))
					r.getDevices().Add(d);
				else
					r.updateDevice(d);
			}
		}
			
	}
	public static int getID()
	{
		return id;
	}
	public static void addRoom(Room r)
	{
		roomList.Add(r);
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
			if(r.getID().Equals(roomID))
				return r;
		}

	return null;
}
}
}