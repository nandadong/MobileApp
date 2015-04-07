using System;

namespace HomeAutomationApp
{
	public class InitParameters
	{

		public static InitParameters _instance = null;

		public static InitParameters getInstance() {
			return _instance;
		}

		public static void setInstance(string mode, string config, string timeline, string user, string password) {
			if (_instance == null)
				_instance = new InitParameters (mode, config, timeline, user, password);
		}
			
		public string Mode 	{ get; private set; }
		public string Config 	{ get; private set; }
		public string Timeline { get; private set; }
		public string User 	{ get; private set; }
		public string Password { get; private set; }


		private InitParameters (string mode, string config, string timeline, string user, string password)
		{
			Mode = mode;
			Config = config;
			Timeline = timeline;
			User = user;
			Password = password;
		}
	}
}

