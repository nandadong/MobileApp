using NUnit.Framework;
using System;
using HomeAutomationApp;
using System.Collections.Generic;
using api;

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
		public void TestGCMRegister ()
		{
			Assert.IsTrue (true);
		}

		
		[Test()]
		public void TestUpdatePosition ()
		{
			UpdateLocationSimModel.JsonGps jGps = new UpdateLocationSimModel.JsonGps();
			UpdateLocationSimModel.UpdatePositonBlob blob = new UpdateLocationSimModel.UpdatePositonBlob();
			UpdateLocationSimModel.JsonEvents jE = new UpdateLocationSimModel.JsonEvents();
			UpdateLocationSimModel.JsonTimeFrame jTF = new UpdateLocationSimModel.JsonTimeFrame();
			UpdateLocationSimModel.JsonTimeline jTL = new UpdateLocationSimModel.JsonTimeline();

			jGps.lat = 98.543;
			jGps.lon = 84.346;
			jGps.alt = 45.3454;

			blob.lat = 98.543;
			blob.lon = 84.346;
			blob.alt = 45.3454;
			blob.time = new DateTime (2015, 11, 15, 9, 59, 44, 345);

		    Assert.IsTrue(jGps.lat.Equals(98.543));
			Assert.IsTrue(jGps.lon.Equals(84.346));
			Assert.IsTrue(jGps.alt.Equals(45.3454));

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
		int houseID = 0;
		var testHouse = new House(houseID);
		string serverAddr = "http://52.1.192.214/";
		Interfaces inter = new Interfaces(new Uri(serverAddr));
		for(int i = 0; i < 10; i++) //add some rooms to the house
		{
			testHouse.addRoom(new Room(i));
		}

		Assert.IsTrue(testHouse.getRooms().Count.Equals(10)); //make sure they were all added

		foreach(Room r in testHouse.getRooms()) //add all the devices in the house to the rooms
		{
			testHouse.getRoom(r.getID()).addAllDevices(inter.getDevices((ulong)testHouse.getID()));
		}

		foreach(Room r in testHouse.getRooms())
		{
			Assert.Greater(r.getDevices().Count, 0);
		}

	}
	}
}
