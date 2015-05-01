using System;
using System.Collections.Generic;
using api;

namespace HomeAutomationApp
{
//The point of the invalidation helper is to simulate how the invalidation would work
//in the automation system. This class contains data that will allow me to get a working 
//invalidation while other teams (device and server) are still working on their API's
public class AffectedDevices{
	public Dictionary<string, string> ID { get; set; }
	public string Type { get; set; }
	public string Name { get; set; }
	public bool Enabled { get; set; }
	public long State { get; set; }
	public int SetPoint { get; set; }
	public int Value { get; set; }
}
public class InvalidationHelper
{
	public List<string> affectedDevices = new List<string>();
	public List<Device> updatedDevice = new List<Device>();
		
	public InvalidationHelper()
	{
		//ID is HouseID, RoomID, DeviceID
		string json = @"{
			'ID': {
				""houseID"": ""0"",""deviceID"": ""1"",""roomID"": ""2""
				},
			'Type': 'GarageDoor',
			'Name': 'Garage1',
			'Enabled': 'true',
			'State': '1',
			'SetPoint': '70',
			'Value': '20'
			}";

			string json2 = @"{
			'ID': {
				""houseID"": ""0"",""deviceID"": ""2"",""roomID"": ""2""
				},
			'Type': 'Light',
			'Name': 'Light1',
			'Enabled': 'true',
			'State': '1',
			'SetPoint': '70',
				'Value': '20'
			}";

		string json3 = @"{
			'ID': {
				""houseID"": ""0"",""deviceID"": ""2"",""roomID"": ""2""
				},
			'Type': 'Light',
			'Name': 'Light1',
			'Enabled': 'false',
			'State': '0',
			'SetPoint': '70',
				'Value': '20'
			}";
			
		affectedDevices.Add(json);
		affectedDevices.Add(json2);
		affectedDevices.Add(json3);
	
		
	}
}
}

