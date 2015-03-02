using System;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

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
			var activity = this.Context as Activity;

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

			var button = view.FindViewById<Android.Widget.Button> (Resource.Id.notifyBtn);
			button.Click += (object sender, EventArgs btnevent) => {
				// Publish the notification:
				const int notificationId = 0;
				notificationManager.Notify (notificationId, notification);
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

