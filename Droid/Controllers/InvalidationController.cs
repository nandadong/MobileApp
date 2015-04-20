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
			var request = HttpWebRequest.Create(string.Format(@"http://serverapi1.azurewebsites.net/api/app/device", rxcui));
			request.ContentType = "application/json";
			request.Method = "GET";

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if(response.StatusCode != HttpStatusCode.OK)
					return "not good";
				else
					return (((HttpWebResponse)response).StatusDescription);
			}
		}
	}
}

