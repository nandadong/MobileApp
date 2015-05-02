using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using api;

namespace HomeAutomationApp
{
public class AddDeviceModel
{
	// TODO: temporary flag to bypass device API while it's stubbed
	bool bypassDeviceApi = true;

	// TODO: this is a hack - replace houseId with actual value!
	const UInt64 houseId = 2;
	// Interfaces DeviceInterface = new Interfaces(new Uri(ConfigModel.Url));

	// variables used in view
	public string tabTitle;
	public string debugLabel;
	public string registerLabel;
	public bool isDeviceListEmpty;
	public List<string> unregisteredDeviceList;
	public string namePlaceholder;
	public string roomPlaceholder;
	public string buttonText;

	// constructor 
	public AddDeviceModel()
	{
		tabTitle = "Devices";
		debugLabel = "";
		registerLabel = "Choose a device, and enter desired values!";


		if(getUnregisteredDevices() != null)
		{
			isDeviceListEmpty = false;
			unregisteredDeviceList = getUnregisteredDevices();
		}
		else
		{
			isDeviceListEmpty = true;
		}

		namePlaceholder = "Device Name";
		roomPlaceholder = "Room Number (Optional)";
		buttonText = "Register Device";

	}

	// calls device API to retrieve list of unregistered devices
	public List<string> getUnregisteredDevices()
	{
		if(bypassDeviceApi)
		{
			List<string> bypassedList = new List<string>();
			bypassedList.Add("LightSwitch #1324");
			bypassedList.Add("LightSwitch #9876");
			bypassedList.Add("Thermostat #1234");
			return bypassedList;
		}
		else
		{
			// call the device API to access functions for devices
			// return DeviceInterface.enumerateDevices(houseId);
		}
		return null;
	}

	// calls device API to register a device
	public api.Device registerDevice(string name, UInt64 room, string info)
	{
		if(bypassDeviceApi)
		{
			// the device will not actually be added if bypassed
			api.Device customDevice = null;
			return customDevice;

		}
		else
		{
			// return DeviceInterface.registerDevice(name, houseId, info);
		}
		return null;
	}
		
}
}
