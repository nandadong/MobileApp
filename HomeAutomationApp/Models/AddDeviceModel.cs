using System;
using System.Collections.Generic;
using api;

namespace HomeAutomationApp
{
public class AddDeviceModel
{
	// TODO: this is a hack - replace houseId with actual value!
	const UInt64 houseId = 2;
	Interfaces DeviceInterface = new Interfaces(new Uri(ConfigModel.Url));

	// variables used in view
	public string tabTitle;
	public string debugLabel;
	public string registerLabel;
	public bool isDeviceListEmpty;
	public List<string> unregisteredDeviceList;
	public string namePlaceholder;
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
		buttonText = "Register Device";

	}

	// calls device API to retrieve list of unregistered devices
	public List<string> getUnregisteredDevices()
	{
		// call the device API to access functions for devices
		return DeviceInterface.enumerateDevices(houseId);
	}

	// calls device API to register a device
	public api.Device registerDevice(string name, string info)
	{
		return DeviceInterface.registerDevice(name, houseId, info);
	}
		
}
}
