using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;


using System;
using System.Collections.Generic;
using api;

namespace HomeAutomationApp
{
public class AddDeviceController
{

	// TODO: this is a hack - replace houseId with actual value!
	const UInt64 houseId = 2;
	Interfaces DeviceInterface = new Interfaces(new Uri(ConfigModel.Url));
	// constructor 
	public AddDeviceController()
	{
		
	}

	// calls device API to retrieve list of unregistered devices
	public static string SendPhysicalChangeAsync(string name, string info)
	{
	    //The hardcoded value is there to accomodate the shortcomings of the Simulation harness blob
	    const UInt64 houseId = 2;
	    // calls device API to register a device
		DeviceInterface.registerDevice(name, houseId, info);
		string return_value = "Device registered. As of now the implemented functionwill not return any value"

    }
}
}
