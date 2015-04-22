using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;


namespace HomeAutomationApp
{
public class UpdatePositionController
{
	public UpdatePositionController()
	{
	}

	public HttpResponseMessage SendPositionAsync(string packet, string user)
	{
		try
		{
			var client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(10);

			Debug.WriteLine("HAD: URL: " + ConfigModel.Url);
			client.BaseAddress = new Uri(ConfigModel.Url);


			var response = client.PostAsync("api/app/user/updateposition/" + user, 
				new StringContent(packet, Encoding.UTF8, "application/json")).Result;

			return response;
		}

		catch(Exception e)
		{
			Debug.WriteLine("HAD - Position Update Error: " + e.Message);
			Debug.WriteLine("HAD - Position Update Error: " + e.InnerException.Message);
		}

		return null;
	}
}
}

