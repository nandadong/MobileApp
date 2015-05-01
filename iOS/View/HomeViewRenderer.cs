using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof(HomeAutomationApp.HomeView), typeof(HomeAutomationApp.iOS.HomeViewRenderer))]

namespace HomeAutomationApp.iOS
{

public class HomeViewRenderer : PageRenderer
{
	protected override void OnElementChanged(VisualElementChangedEventArgs e)
	{
		base.OnElementChanged(e);

		var page = e.NewElement as HomeView;

		var hostViewController = ViewController;

		var viewController = new HomeViewController();

		var label = new UILabel(new CGRect(0, 40, 320, 40));
		label.Text = "Hello There!";
		viewController.View.Add(label);

		hostViewController.AddChildViewController(viewController);
		hostViewController.View.Add(viewController.View);

		viewController.DidMoveToParentViewController(hostViewController);
	}
}
}

