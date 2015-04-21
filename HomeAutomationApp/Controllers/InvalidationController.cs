using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using api;


namespace HomeAutomationApp
{
	public static class InvalidationController
	{
		public static string invalidate()
		{
			Task<HttpStatusCode> code = getAllUpdatedDevices();
			code.Wait(-1);
			//Assert.That(!code.Equals(null));
			//Assert.That(!(code.GetType().Name.Equals(null)));

			//int houseID = 0;
			House.createHouse(0);
			string serverAddr = "http://52.1.192.214/";
			Interfaces inter = new Interfaces(new Uri(serverAddr));
			for(int i = 0; i < 10; i++) //add some rooms to the house
			{
				House.addRoom(new Room(i));
			}

			//Assert.IsTrue(House.getRooms().Count.Equals(10)); //make sure they were all added
			House.updateHouse(inter.getDevices((ulong)House.getID()));

			/*foreach(Room r in House.getRooms())
			{
				Assert.Greater(r.getDevices().Count, 0);
			}*/
			return code.Result.ToString();
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

