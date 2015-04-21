using api;
using System.Collections.Generic;

namespace HomeAutomationApp
{
public class Room
{
	private int id;
	private List<Device> deviceList;
	public Room(int roomID)
	{
		id = roomID;
		deviceList = new List<Device>();
	}

	public void addDevice(Device d)
	{
		deviceList.Add(d);
	}

	public void addAllDevices(List<Device> devices)
	{
		deviceList.AddRange(devices);
	}

	public void updateDevice(Device updatedDevice)
	{
		foreach(Device d in deviceList){
			if(d.ID.DeviceID.Equals(updatedDevice.ID.DeviceID))
			{
				deviceList.RemoveAt(deviceList.IndexOf(d));
				deviceList.Insert(deviceList.IndexOf(d),updatedDevice);
			}
		}
	}

	public void removeDevice(Device d)
	{
		foreach(Device k in deviceList)
		{
			if(k.Equals(d))
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
			if(d.ID.DeviceID.Equals(id))
				return d;
		}

		return null;
	}
}
}


