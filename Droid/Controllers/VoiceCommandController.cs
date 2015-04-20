using System;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;

namespace HomeAutomationApp.Droid
{
public class VoiceCommandController
{
	public VoiceCommandController()
	{
	}

	//handles sending a user coordinate json blob to the decision system.
	public void makeItBrighterNearMe()
	{

		JObject blob = new JObject();
		String timeStamp = GetTimestamp(DateTime.Now);

		blob["lat"] = 98.543;
		blob["long"] = 84.345;
		blob["alt"] = 45.3454;
		blob["time"] = timeStamp;

		POST(blob, "api/app/user/brighten");

	}

	public static String GetTimestamp(DateTime value)
	{
		return value.ToString("yyyyMMddHHmmssfff");
	}

	//POSTs a json blob to the server API
	public string POST(JObject blob, string url)
	{
		// Create a request using a URL that can receive a post. 
		WebRequest request = WebRequest.Create(url);
		// Set the Method property of the request to POST.
		request.Method = "POST";
		// Create POST data and convert it to a byte array.
		string postData = blob.ToString();
		byte[] byteArray = Encoding.UTF8.GetBytes (postData);
		// Set the ContentType property of the WebRequest.
		request.ContentType = "application/x-www-form-urlencoded";
		// Set the ContentLength property of the WebRequest.
		request.ContentLength = byteArray.Length;
		// Get the request stream.
		Stream dataStream = request.GetRequestStream ();
		// Write the data to the request stream.
		dataStream.Write (byteArray, 0, byteArray.Length);
		// Close the Stream object.
		dataStream.Close ();
		// Get the response.
		WebResponse response = request.GetResponse ();
		// Display the status.
		Console.WriteLine (((HttpWebResponse)response).StatusDescription);
		// Get the stream containing content returned by the server.
		dataStream = response.GetResponseStream ();
		// Open the stream using a StreamReader for easy access.
		StreamReader reader = new StreamReader (dataStream);
		// Read the content.
		string responseFromServer = reader.ReadToEnd ();
		// Display the content.
		Console.WriteLine (responseFromServer);
		// Clean up the streams.
		reader.Close ();
		dataStream.Close ();
		response.Close ();

		return responseFromServer;
	}
}
}

