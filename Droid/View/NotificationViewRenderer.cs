using System;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Gcm.Client;

[assembly:ExportRenderer(typeof(HomeAutomationApp.NotificationView), typeof(HomeAutomationApp.Droid.NotificationViewRenderer))]

namespace HomeAutomationApp.Droid
{
	public class NotificationViewRenderer : PageRenderer
	{
		Android.Views.View view;

		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			// this is a ViewGroup - so should be able to load an AXML file and FindView<>
			var activity = this.Context as MainActivity;

			var o = activity.LayoutInflater.Inflate(Resource.Layout.NotificationLayout, this, false);
			view = o;

			// Instantiate the builder and set notification elements:
			Notification.Builder builder = new Notification.Builder (this.Context)
				.SetContentTitle ("Home Automation")
				.SetContentText ("Room Temperature is 72 F")
				.SetSmallIcon (Resource.Drawable.icon)
				.SetOngoing (true);

			// Build the notification:
			Notification notification = builder.Build();

			// Get the notification manager:
			NotificationManager notificationManager = this.Context.GetSystemService (Context.NotificationService) as NotificationManager;

			var deviceID = view.FindViewById<TextView> (Resource.Id.deviceID);
			var button = view.FindViewById<Android.Widget.Button> (Resource.Id.notifyBtn);
			var registerBtn = view.FindViewById<Android.Widget.Button> (Resource.Id.GCMRegister);
			var unregisterBtn = view.FindViewById<Android.Widget.Button> (Resource.Id.GCMUnregister);
			var refreshBtn = view.FindViewById<Android.Widget.Button>(Resource.Id.refresh);
			
			if(GCMModel.isRegistered())
				deviceID.Text = GCMModel.getDeviceID();
			else
				deviceID.Text = "Not Registered";

			button.Click += (object sender, EventArgs btnevent) => {
//				// Publish the notification:
//				const int notificationId = 0;
//				notificationManager.Notify (notificationId, notification);
				GCMModel.SendNotifyAsync();
			};


			registerBtn.Click += (object sender, EventArgs btnevent) => {
				GCMModel.register();
				deviceID.Text = GCMModel.getDeviceID();
			};

			unregisterBtn.Click += (object sender, EventArgs btnevent) => {
				GCMModel.unregister();
				deviceID.Text = "Not Registered";
			};

			refreshBtn.Click += (object sender, EventArgs btnevent) => {
				deviceID.Text = GCMModel.getDeviceID();
			};


			AddView(view);
		}

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);
			var msw = MeasureSpec.MakeMeasureSpec (r - l, MeasureSpecMode.Exactly);
			var msh = MeasureSpec.MakeMeasureSpec (b - t, MeasureSpecMode.Exactly);
			view.Measure(msw, msh);
			view.Layout (0, 0, r - l, b - t);
		}
	}
}

