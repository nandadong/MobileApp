using System;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;


namespace HomeAutomationApp.Droid
{
	public class InvalidationController
	{
		public InvalidationController()
		{
		}

		public void InvalidateDevices(string json)
		{
			
		}

		public string getAllUpdatedDevices()
		{
			var rxcui = "198440";
			var request = HttpWebRequest.Create(string.Format(@"http://.../ api/app/device", rxcui));
			request.ContentType = "application/json";
			request.Method = "GET";

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if (response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					var content = reader.ReadToEnd();
					if(string.IsNullOrWhiteSpace(content)) {
						Console.Out.WriteLine("Response contained empty body...");
					return null;
					}
					else {
						Console.Out.WriteLine("Response Body: \r\n {0}", content);
						return content;
					}
				}
			}
		}
	}
}