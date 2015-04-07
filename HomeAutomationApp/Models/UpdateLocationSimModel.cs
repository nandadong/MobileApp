using System;
using System.Collections.Generic;

namespace HomeAutomationApp
{
	public static class UpdateLocationSimModel
	{
		// classes for timeline blob
		public class JsonGps
		{
			public double lat  { get; set; }
			public double lon  { get; set; }
			public double alt  { get; set; }
		}

		public class UpdatePositonBlob
		{
			public double lat  { get; set; }
			public double lon { get; set; }
			public double alt  { get; set; }
			public DateTime time { get; set; }
		}

		public class JsonEvents
		{
			public DateTime time { get; set; }
			public string key { get; set; }
			public JsonGps value { get; set; }
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

