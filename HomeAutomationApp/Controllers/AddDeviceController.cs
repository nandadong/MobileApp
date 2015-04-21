using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using api;

namespace HomeAutomationApp
{
public class AddDeviceController
{



	// constructor 
	public AddDeviceController()
	{

	}

	// calls device API to retrieve list of unregistered devices
	public static string SendDeviceChangeAsync(string name, string info)
	{
		//The hardcoded value is there to accomodate the shortcomings of the Simulation harness blob
		const UInt64 houseId = 2;
		// calls device API to register a device
		Interfaces DeviceInterface1 = new Interfaces(new Uri(ConfigModel.Url));
		DeviceInterface1.registerDevice(name, houseId, info);
		string return_value = "Device registered. As of now the implemented function will not return null";
		return return_value;
	}
}
}