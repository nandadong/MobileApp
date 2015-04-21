using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;


namespace HomeAutomationApp
{
public class AddDeviceController
{
	public AddDeviceController()
	{
	}

	public async Task<HttpStatusCode> SendDeviceAsync(string packet, string user)
	{

		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(2);

		client.BaseAddress = new Uri(ConfigModel.Url);

		try
		{
			var response = await client.PostAsync("api/device" + user, 
				new StringContent(packet, Encoding.UTF8, "application/json")).ConfigureAwait(false);

			return response.StatusCode;

		}
		catch(Exception e)
		{
			Debug.WriteLine("HAD - Position Update Error: " + e.Message);
			Debug.WriteLine("HAD - Position Update Error: " + e.InnerException.Message);
		}

		return HttpStatusCode.InternalServerError;
	}
}
}

