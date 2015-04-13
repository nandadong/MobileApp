using NUnit.Framework;
using System;
using HomeAutomationApp;
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

			Assert.True(jGps.lat.Equals(98.543));
			Assert.True(jGps.lon.Equals(84.346));
			Assert.True(jGps.alt.Equals(45.3454));

			Assert.True(blob.lat.Equals(98.543));
			Assert.True(blob.lon.Equals(84.346));
			Assert.True(blob.alt.Equals(45.3454));
			Assert.True(blob.time.Equals(new DateTime (2015, 11, 15, 9, 59, 44, 345)));

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
	}
}

