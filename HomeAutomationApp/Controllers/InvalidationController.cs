using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;


namespace HomeAutomationApp
{
	public static class InvalidationController
	{
		public static HttpStatusCode invalidate()
		{
			Task<HttpStatusCode> code = getAllUpdatedDevices();
			code.Wait();
			return code.Result;
		}

	public static async Task<HttpStatusCode> getAllUpdatedDevices()
	{

		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(2);

		client.BaseAddress = new Uri(ConfigModel.Url);

		try
		{
			var response = await client.GetAsync("http://serverapi1.azurewebsites.net/api/app/device/");

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

