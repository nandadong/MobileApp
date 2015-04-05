using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace HomeAutomationApp.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ("1", "2", "3", "4", "5"));

			return base.FinishedLaunching (app, options);
		}
	}
}

