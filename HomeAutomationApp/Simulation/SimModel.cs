using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomeAutomationApp
{
public static class SimModel
{
	public abstract class JsonValue
	{
	}

	// classes for timeline blob
	public class JsonGps : JsonValue
	{
		public double lat  { get; set; }

		public double lon  { get; set; }

		public double altitude  { get; set; }
	}

	public class JsonPhysicalChange : JsonValue
	{
		public int deviceid  { get; set; }

		public string type  { get; set; }

		public double value  { get; set; }
	}

	public class JsonVoiceChange : JsonValue
	{
		public double lat  { get; set; }

		public double lon  { get; set; }

		public double altitude  { get; set; }

		public string action { get; set; }
	}

	public class JsonAddDevice : JsonValue
	{
		public string type  { get; set; }

		public int roomid  { get; set; }

		public string name  { get; set; }
	}
	
	public class JsonAddRoom : JsonValue
	{
	    public int houseid { get; set; }
		
		public string name { get; set; }
		
	}

	public class UpdatePositonBlob
	{
		public double lat  { get; set; }

		public double lon { get; set; }

		public double alt  { get; set; }

		public DateTime time { get; set; }

		public string userID {get; set;}
	}

	public class JsonEvents
	{
		public DateTime time { get; set; }

		public string key { get; set; }

		public JsonValue value { get; set; }
	}

	public class JsonTimeFrame
	{
		public DateTime wall { get; set; }

		public DateTime sim { get; set; }

		public double rate { get; set; }
	}

	public class JsonTimeline
	{
		public JsonTimeFrame timeFrame { get; set; }

		public List<JsonEvents> events { get; set; }
	}

}
}

