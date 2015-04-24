using api;
using System.Collections.Generic;

namespace HomeAutomationApp
{
public class Room
{
	private int id;
	private static List<Device> deviceList;
	public Room(int roomID)
	{
		id = roomID;
		deviceList = new List<Device>();
	}

	public static bool inRoom(Device dev){
		foreach(Device d in deviceList)
		{
			if(d.ID.DeviceID == dev.ID.DeviceID)
				return true;
		}

		return false;
	}

	public void addDevice(Device d)
	{
		if(!inRoom(d))
			deviceList.Add(d);
	}

	public void addAllDevices(List<Device> devices)
	{
		deviceList.AddRange(devices);
	}

	public void updateDevice(Device updatedDevice)
	{
		for(int i = 0; i < deviceList.Count; i++){
			if(deviceList[i].ID.DeviceID == updatedDevice.ID.DeviceID)
			{
				deviceList[i] = updatedDevice;
			}
		}
	}

	public void removeDevice(Device d)
	{
		foreach(Device k in deviceList)
		{
			if(k.ID.DeviceID == d.ID.DeviceID)
				deviceList.Remove(d);
		}
	}
	public int getID()
	{
		return id;
	}

	public List<Device> getDevices()
	{
		return deviceList;
	}

	public Device getDevice(int id){
		foreach(Device d in deviceList)
		{
			if(d.ID.DeviceID == (ulong)id)
				return d;
		}

		return null;
	}
}
}


