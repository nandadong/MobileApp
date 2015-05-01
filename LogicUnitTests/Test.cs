using NUnit.Framework;
using System;
using HomeAutomationApp;
using System.Collections.Generic;
using api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace LogicUnitTests
{
[TestFixture ()]
public class Test
{
	[Test ()]
	public void TestCase ()
	{
		Assert.IsTrue (true);
	}

	[Test ()]
	public void TestMakeItBrighter()
	{
		var voiceController = new VoiceCommandController(); 
		JObject blob = new JObject();
		var time = new DateTime();
		String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
		const string user = "user";
		blob["lat"] = 98.543;
		blob["long"] = 84.345;
		blob["alt"] = 45.3454;
		blob["time"] = timeStamp;
		Assert.IsTrue(voiceController.makeItBrighterNearMe(blob.ToString(), user).Equals("OK"));
	}

	[Test ()]
	public void TestPasswordHash()
	{
		const string password = "monkey";
		User.setPassword(password);
		Assert.IsTrue(User.TestCrypt(password));
	}




	[Test()]
	public void TestUpdatePosition ()
	{
		SimModel.JsonGps jGps = new SimModel.JsonGps();
		SimModel.UpdatePositonBlob blob = new SimModel.UpdatePositonBlob();
		SimModel.JsonEvents jE = new SimModel.JsonEvents();
		SimModel.JsonTimeFrame jTF = new SimModel.JsonTimeFrame();
		SimModel.JsonTimeline jTL = new SimModel.JsonTimeline();

		jGps.lat = 98.543;
		jGps.lon = 84.346;
		jGps.altitude = 45.3454;

		blob.lat = 98.543;
		blob.lon = 84.346;
		blob.alt = 45.3454;
		blob.time = new DateTime (2015, 11, 15, 9, 59, 44, 345);

		/*jE.time = new DateTime (2015, 11, 15, 9, 59, 44, 345);
		jE.key = "Event:";
		jE.value = 8;*/

		Assert.IsTrue(jGps.lat.Equals(98.543));
		Assert.IsTrue(jGps.lon.Equals(84.346));
		Assert.IsTrue(jGps.altitude.Equals(45.3454));

		Assert.IsTrue(blob.lat.Equals(98.543));
		Assert.IsTrue(blob.lon.Equals(84.346));
		Assert.IsTrue(blob.alt.Equals(45.3454));
		Assert.IsTrue(blob.time.Equals(new DateTime (2015, 11, 15, 9, 59, 44, 345)));
	}

	[Test()]
	public void TestCommandLineArguments()
	{
		const string jsonTimelineString =
			"{" +
			"	timeFrame : {" +
			"	    wall: \"1997-07-16T19:20:30+01:00\"," +
			"	    sim: \"1997-07-16T19:20:30+01:00\"," +
			"	    rate: 2.0" +
			"   }," +
			"	events : [" +
			"		{" +
			"			time : \"1997-07-16T19:20:30+01:00\"," +
			"			key : \"locationChange\"," +
			"		value : {" +
			"				lat : 1.123456," +
			"				lon : 2.123456," +
			"				alt : 3.123456" +
			"		}" +
			"		}" +
			"	]" +
			"}";

		const string jsonConfigString = 
			"{" +
			"serverLocation : \"http://52.1.192.214/\" " +
			"}";
		InitParameters.setInstance ("SIM", jsonConfigString, jsonTimelineString, "user1", "password");

		/*var configObj = JsonConvert.DeserializeObject<JObject> (config);
			ConfigModel.Url = configObj.GetValue ("serverLocation").ToString();*/
		Assert.IsTrue (InitParameters.getInstance().Mode.Equals ("SIM"));
		Assert.IsTrue (InitParameters.getInstance().User.Equals ("user1"));
		Assert.IsTrue (InitParameters.getInstance().Timeline.Equals ("{" +
			"	timeFrame : {" +
			"	    wall: \"1997-07-16T19:20:30+01:00\"," +
			"	    sim: \"1997-07-16T19:20:30+01:00\"," +
			"	    rate: 2.0" +
			"   }," +
			"	events : [" +
			"		{" +
			"			time : \"1997-07-16T19:20:30+01:00\"," +
			"			key : \"locationChange\"," +
			"		value : {" +
			"				lat : 1.123456," +
			"				lon : 2.123456," +
			"				alt : 3.123456" +
			"		}" +
			"		}" +
			"	]" +
			"}"));
		Assert.IsTrue (InitParameters.getInstance().Password.Equals ("password"));
	}

	[Test()]
	public void TestGetDevicesAndRegister()
	{
		// get the list of devices and check that it is not null
		AddDeviceModel testDeviceModel = new AddDeviceModel();
		List<string> deviceList = testDeviceModel.getUnregisteredDevices();
		Assert.IsTrue(!deviceList.Equals(null));

		// select the first device returns, and register it with a new name
		api.Device registeredDevice = testDeviceModel.registerDevice("test name", deviceList[0]);
		Assert.IsTrue(!registeredDevice.Equals(null));
	}

	[Test()]
	public void TestHouseRoomsDevices() //this test simulates what would happen during a room invalidation
	{
		int houseID = 0; //set a house ID
		House.createHouse(houseID); //create a house
		string serverAddr = "http://serverapi1.azurewebsites.net/";
		Interfaces inter = new Interfaces(new Uri(serverAddr)); 
		for(int i = 0; i < 10; i++) //add some rooms to the house
		{
			House.addRoom(new Room(i));
		}

		Assert.IsTrue(House.getRooms().Count.Equals(10)); //make sure they were all added
		List<Device> devices = new List<Device>();
		Random ran = new Random();
		IDeviceInput k = new HouseInput("","");
		IDeviceOutput u = new HouseOutput("","");
		Hats.Time.TimeFrame t = new Hats.Time.TimeFrame();

		for(int i = 0; i < 10; i++) //populate a device list, simulating what we receive from the server
		{
			devices.Add(new GarageDoor(k, u,t));
			FullID id = new FullID();
			int roomID = ran.Next(0, 9);
			Console.WriteLine("Generated Random = " + roomID);
			id.DeviceID = (ulong)i;
			id.RoomID = (ulong)roomID;
			id.HouseID = (ulong)houseID;
			devices[i].ID = id;
		}

		House.updateHouse(devices); //update the houses devices (add them if they dont exist) //inter.getDevices((ulong)House.getID()));
		int counter = 0;
		bool deviceInHouseAndEnabled = false;
		int roomNum = ran.Next(0, 9);
		int deviceNum = ran.Next(0, 9);
		GarageDoor garage;
		foreach(Room r in House.getRooms()) //for every room in the house
		{
			garage = (GarageDoor)r.getDevice(deviceNum); //select a device at random
			if(garage.ID.DeviceID == (ulong)deviceNum && garage.Enabled == true) //if we got the device we expected
			{
				deviceInHouseAndEnabled = true; //change the test parameter to true
				roomNum = r.getID(); //get the room id
			}
			if(r.getDevices().Count > 0)
			{
				counter++;
			}

		}

		Assert.Greater(counter, 0);
		Assert.True(deviceInHouseAndEnabled);

	}

	[Test()]
	public void testInvalidationHelper()
	{
		InvalidationHelper help = new InvalidationHelper();
		AffectedDevices ad = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[0]);
		AffectedDevices ad2 = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[1]);
		AffectedDevices ad3 = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[2]);

		Assert.IsTrue(!ad.Name.Equals(null));
		Assert.IsTrue(!ad2.Name.Equals(null));
		Assert.IsTrue(!ad3.Name.Equals(null));

		Assert.IsTrue(ad.Name.Equals("Garage1"));
		Assert.IsTrue(ad.Type.Equals("GarageDoor"));
		Assert.IsTrue(ad.Enabled.Equals(true));
		Assert.IsTrue(ad.State.Equals(1));
		Assert.IsTrue(ad.SetPoint.Equals(70));
		Assert.IsTrue(ad.Value.Equals(20));

		Assert.IsTrue(ad2.Name.Equals("Light1"));
		Assert.IsTrue(ad2.Type.Equals("Light"));
		Assert.IsTrue(ad2.Enabled.Equals(true));
		Assert.IsTrue(ad2.State.Equals(1));
		Assert.IsTrue(ad2.SetPoint.Equals(70));
		Assert.IsTrue(ad2.Value.Equals(20));

		Assert.IsTrue(ad3.Name.Equals("Light1"));
		Assert.IsTrue(ad3.Type.Equals("Light"));
		Assert.IsTrue(ad3.Enabled.Equals(true));
		Assert.IsTrue(ad3.State.Equals(0));
		Assert.IsTrue(ad3.SetPoint.Equals(70));
		Assert.IsTrue(ad3.Value.Equals(20));
	}

	[Test()]
	public void invalidationProcess(){
		InvalidationHelper help = new InvalidationHelper();
		AffectedDevices ad = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[0]);
		AffectedDevices ad2 = JsonConvert.DeserializeObject<AffectedDevices>(help.affectedDevices[1]);
		Assert.IsTrue(!ad.Name.Equals(null));
		Assert.IsTrue(!ad2.Name.Equals(null));

		List<Device> devices = new List<Device>();

		IDeviceInput k = new HouseInput("","");
		IDeviceOutput u = new HouseOutput("","");
		Hats.Time.TimeFrame t = new Hats.Time.TimeFrame();

		GarageDoor gd = new GarageDoor(k, u, t);
		gd.Enabled = ad.Enabled;
		gd.ID.RoomID = Convert.ToUInt64(ad.ID["roomID"]);
		gd.ID.DeviceID = Convert.ToUInt64(ad.ID["deviceID"]);
		gd.Enabled = ad.Enabled;
		devices.Add(gd);

		House.createHouse(Convert.ToInt32(ad.ID["houseID"]));
		House.updateHouse(devices);


		Console.WriteLine(ad.Name);
	}

	[Test()]
	public void TestInvalidation() //this test simulates what would happen during a room invalidation
	{
		/*var code = InvalidationController.invalidate();
		Assert.That(!code.Equals(null));
		Assert.That(!(code.GetType().Name.Equals(null)));
		Console.WriteLine(code);*/

		int houseID = 0; //set a house ID
		House.createHouse(houseID); //create a house
		string serverAddr = "http://serverapi1.azurewebsites.net/";
		Interfaces inter = new Interfaces(new Uri(serverAddr)); 
		for(int i = 0; i < 10; i++) //add some rooms to the house
		{
			House.addRoom(new Room(i));
		}

		Assert.IsTrue(House.getRooms().Count.Equals(10)); //make sure they were all added
		List<Device> devices = new List<Device>();
		Random ran = new Random();
		IDeviceInput k = new HouseInput("","");
		IDeviceOutput u = new HouseOutput("","");
		Hats.Time.TimeFrame t = new Hats.Time.TimeFrame();

		for(int i = 0; i < 10; i++) //populate a device list, simulating what we receive from the server
		{
			devices.Add(new GarageDoor(k, u,t));
			FullID id = new FullID();
			int roomID = ran.Next(0, 9);
			//Console.WriteLine("Generated Random = " + roomID);
			id.DeviceID = (ulong)i;
			id.RoomID = (ulong)roomID;
			id.HouseID = (ulong)houseID;
			devices[i].ID = id;
		}
			
		House.updateHouse(devices); //update the houses devices (add them if they dont exist) //inter.getDevices((ulong)House.getID()));
		int counter = 0;
		bool deviceInHouseAndEnabled = false;
		int roomNum = ran.Next(0, 9);
		int deviceNum = ran.Next(0, 9);
		GarageDoor garage;
		foreach(Room r in House.getRooms()) //for every room in the house
		{
			garage = (GarageDoor)r.getDevice(deviceNum); //select a device at random
			if(garage.ID.DeviceID == (ulong)deviceNum && garage.Enabled == true) //if we got the device we expected
			{
				deviceInHouseAndEnabled = true; //change the test parameter to true
				roomNum = r.getID(); //get the room id
			}
			if(r.getDevices().Count > 0)
			{
				counter++;
			}

		}
			
		Assert.Greater(counter, 0);
		Assert.True(deviceInHouseAndEnabled);

		Assert.True(((GarageDoor)House.getRoom(roomNum).getDevice(deviceNum)).Enabled.Equals(true)); //the garage door is currently enabled

		GarageDoor door = new GarageDoor(k, u,t); //pretend that we just got this GarageDoor device from the server because its value changed
		FullID fid = new FullID(); //fake a device
		fid.DeviceID = (ulong)deviceNum;
		fid.RoomID = (ulong)roomNum;
		fid.HouseID = (ulong)houseID;
		door.ID = fid;
		door.Enabled = false; //set the device to be disabled
		List<Device> dList = new List<Device>();

		dList.Add(door); //invalidate and update our local copy
		House.updateHouse(dList); //update

		//GarageDoor doorTest = (GarageDoor)House.getRoom(roomNum).getDevice(deviceNum); //get the garage door

		Assert.True(((GarageDoor)House.getRoom(roomNum).getDevice(deviceNum)).Enabled.Equals(false));
	}
	[Test()]
	public void TestVoiceChange() //this test simulates what would happen during a room invalidation
	{


		string blob = "Voice change";
		string User = "User 1";
		Assert.IsTrue(VoiceCommandController.SendBrighterAsync(blob, User).Equals(HttpStatusCode.OK));
	}

	[Test()]
	public void TestPhysicalChange() //this test simulates a device change
	{


		string blob1 = "Physical change";
		api.Device dev = null;
		Assert.IsTrue(PhysicalChangeController.SendPhysicalChangeAsync(blob1, dev).Equals(false));
	}
	[Test()]
	public void TestAddDevice() //Voice Command
	{


		string blob1 = "Voice command";
		string name1 = "Unique device";
		AddDeviceController.SendDeviceChangeAsync(blob1, name1);
		Assert.IsTrue(AddDeviceController.SendDeviceChangeAsync(blob1, name1).Equals(true));
	}



}
}




