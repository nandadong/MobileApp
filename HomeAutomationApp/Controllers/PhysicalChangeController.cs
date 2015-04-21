using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using api;

namespace HomeAutomationApp
{
public class PhysicalChangeController
{
	public PhysicalChangeController()
	{
	}

	public static bool SendPhysicalChangeAsync(string packet, string dev)
	{

		bool return_value =api.Interfaces.UpdateDevice(dev, packet);

		return return_value;
	}
}
}
