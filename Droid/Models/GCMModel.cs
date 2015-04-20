using System;
using Gcm.Client;
using Android.Content;

namespace HomeAutomationApp.Droid
{
public class GCMModel
{
	private string deviceID;
	public GCMModel(Context context)
	{
		GcmClient.CheckDevice(context);
		GcmClient.CheckManifest(context);
		if(GcmClient.GetRegistrationId(context).Length != 0)
			deviceID = GcmClient.GetRegistrationId(context);
		else
			deviceID = "";
	}

	public string getDeviceID()
	{
		return deviceID;
	}

	public void setDeviceID(string id)
	{
		deviceID = id;
	}
}
}

