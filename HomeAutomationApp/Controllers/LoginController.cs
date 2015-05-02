using System;
using System.Net.Http;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HomeAutomationApp
{
public static class LoginController
{
	//private bool registered = false;
	public static bool RequestLogin()
	{
		if(getUser(User.getUsername()).Result.IsSuccessStatusCode)
			return true;
		else
			return false;
	}

	public static bool RegisterUser()
	{
		JObject blob = new JObject();
		blob["username"] = User.getUsername();
		blob["password"] = User.getPassword();
		blob["deviceID"] = User.getDeviceID();
		if(SendUserAsync(blob.ToString()).IsSuccessStatusCode)
			return true;
		else
			return false;
	}

	public static HttpResponseMessage SendUserAsync(string packet)
	{
		try
		{
			var client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(10);


			var response = client.PostAsync("http://serverapi1.azurewebsites.net/api/storage/user/", 
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

	public static  Task<HttpResponseMessage> getUser(string user)
	{
		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(10);
		var response = client.GetAsync("http://serverapi1.azurewebsites.net/api/app/device/" + user);
		return response;
	}
}
}

