using System;
using System.Net;
using System.IO;
using System.Json;
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

	public async Task<object> SendPositionAsync(string packet, string user)
	{

		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(2);

		client.BaseAddress = new Uri(ConfigModel.Url);

		try
		{
			var response = await client.PostAsync("api/user/updateposition/" + user, 
				               new StringContent(packet, Encoding.UTF8, "application/json")).ConfigureAwait(false);

			return response.StatusCode;

		}
		catch(Exception e)
		{
			Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.Message);
			Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.InnerException.Message);
		}

		return HttpStatusCode.InternalServerError;
	}
}
}

