using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using OpenEars;
using Foundation;
using OpenEars.Voices;
using System.Drawing;

[assembly: ExportRenderer(typeof(HomeAutomationApp.HomeView), typeof(HomeAutomationApp.iOS.HomeViewRenderer))]

namespace HomeAutomationApp.iOS
{

public class HomeViewRenderer : PageRenderer
{
	#region Fields

	OEEventsObserver observer;
	OEPocketsphinxController pocketSphinxController;
	OEFliteController fliteController;

	Slt slt;

	public readonly string pathToLanguageModel;
	public readonly string pathToDictionary;
	public readonly string pathToAcousticModel;

	UILabel heardTextView;
	UILabel statusTextView;
	UIButton listenButton;

	#endregion

	#region Contructor
	public HomeViewRenderer()
	{
		observer = new OEEventsObserver ();
		observer.Delegate = new MyOpenEarsEventsObserverDelegate (this);
		pocketSphinxController = new OEPocketsphinxController ();
		fliteController = new OEFliteController ();
		slt = new Slt();

		string[] names = { "MAKE", "IT", "BRIGHTER", "NEAR", "ME", "" };

		pathToAcousticModel = NSBundle.MainBundle.ResourcePath + System.IO.Path.DirectorySeparatorChar + "AcousticModelEnglish.bundle";

		OELanguageModelGenerator generator = new OELanguageModelGenerator();
		NSError error = generator.GenerateLanguageModelFromArray(NSArray.FromStrings(names), "HomeAutomationLanguageModel", pathToAcousticModel);

		if(error != null)
		{
			Console.WriteLine("Failed to generate model from array. Adopting default model.");
			pathToLanguageModel = NSBundle.MainBundle.ResourcePath + System.IO.Path.DirectorySeparatorChar + "OpenEars1.languagemodel";
			pathToDictionary = NSBundle.MainBundle.ResourcePath + System.IO.Path.DirectorySeparatorChar + "OpenEars1.dic";
		}
		else
		{
			pathToLanguageModel = generator.PathToSuccessfullyGeneratedLanguageModelWithRequestedName("HomeAutomationLanguageModel");
			pathToDictionary = generator.PathToSuccessfullyGeneratedDictionaryWithRequestedName("HomeAutomationLanguageModel");
		}

	}
	#endregion

	#region ViewController Overrides
	public override void DidReceiveMemoryWarning()
	{
		// Releases the view if it doesn't have a superview.
		base.DidReceiveMemoryWarning();

		// Release any cached data, images, etc that aren't in use.
	}

	protected override void OnElementChanged(VisualElementChangedEventArgs e)
	{
		base.OnElementChanged(e);

		var page = e.NewElement as HomeView;


	}


	public override void ViewDidLoad()
	{
		base.ViewDidLoad();

		float h = 31.0f;
		float w = (float) View.Bounds.Width;

		statusTextView = new UILabel 
		{
			Text = "Status Text Here",
			Frame = new RectangleF(10, 32, w - 20, h)
		};

		heardTextView = new UILabel 
		{
			Text = "Transcribed Text Here",
			Frame = new RectangleF(10, 64, w - 20, h)
		};

		listenButton = UIButton.FromType(UIButtonType.RoundedRect);
		listenButton.Frame = new RectangleF(10, 120, w - 20, 44);
		listenButton.SetTitle("Submit", UIControlState.Normal);


		listenButton.TouchUpInside += async (object sender, EventArgs ev) => {
			StartListening();
		};

		var hostViewController = ViewController;

		var viewController = new UIViewController();

		viewController.View.AddSubview(statusTextView);

		viewController.View.AddSubview(heardTextView);

		viewController.View.AddSubview(listenButton);

		hostViewController.AddChildViewController(viewController);
		hostViewController.View.Add(viewController.View);

		viewController.DidMoveToParentViewController(hostViewController);

		// Perform any additional setup after loading the view, typically from a nib.
	}
	#endregion

	#region Update
	public void UpdateStatus (String text)
	{
		if(statusTextView != null)
			statusTextView.Text = text;
	}

	public void UpdateText (String text)
	{
		if(heardTextView != null)
			heardTextView.Text = text;
	}

	public void UpdateButtonStates (bool hidden1, bool hidden2, bool hidden3, bool hidden4)
	{
		//		startListeningButton.Hidden = hidden1;
		//		stopListeningButton.Hidden = hidden2;
		//		suspendRecognitionButton.Hidden = hidden3;
		//		resumeRecognitionButton.Hidden = hidden4;
	}

	public void Say (String text)
	{
		fliteController.Say (text, slt);
	}

	public void StartListening ()
	{
		pocketSphinxController.StartListeningWithLanguageModelAtPathdictionaryAtPathacousticModelAtPathlanguageModelIsJSGF (
			pathToLanguageModel,
			pathToDictionary,
			pathToAcousticModel,
			false
		);
	}

	public void StopListening ()
	{
		pocketSphinxController.StopListening ();
	}

	public void SuspendRecognition ()
	{
		pocketSphinxController.SuspendRecognition ();
	}

	public void ResumeRecognition ()
	{
		pocketSphinxController.ResumeRecognition ();
	}
	#endregion


	#region Delegate 
	class MyOpenEarsEventsObserverDelegate : OEEventsObserverDelegate
	{
		HomeViewRenderer controller;

		public MyOpenEarsEventsObserverDelegate (HomeViewRenderer ctrl)
		{
			controller = ctrl;
		}

		public override void PocketsphinxDidReceiveHypothesis (NSString hypothesis, NSString recognitionScore, NSString utteranceID)
		{
			controller.UpdateText ("Heard: " + hypothesis + " with Score: " + recognitionScore + " utteranceId: " + utteranceID);
			controller.Say ("You said: " + hypothesis);
		}

		public override void AudioSessionInterruptionDidBegin ()
		{
			Console.WriteLine ("AudioSession interruption began.");
			controller.UpdateStatus ("AudioSession interruption began");
			controller.StopListening ();

		}

		public override void AudioSessionInterruptionDidEnd ()
		{
			Console.WriteLine ("AudioSession interruption end.");
			controller.UpdateStatus ("AudioSession interruption end.");
			controller.StartListening ();
		}

		public override void AudioInputDidBecomeUnavailable ()
		{
			Console.WriteLine ("The audio input has become unavailable");
			controller.UpdateStatus ("The audio input has become unavailable");
			controller.StopListening ();
		}

		public override void AudioInputDidBecomeAvailable ()
		{
			Console.WriteLine ("The audio input is available.");
			controller.UpdateStatus ("The audio input is available");
			controller.StartListening ();
		}

		public override void AudioRouteDidChangeToRoute (NSString newRoute)
		{
			Console.WriteLine ("Audio route change. The new route is " + newRoute);
			controller.UpdateStatus ("Audio route change. The new route is " + newRoute);
			controller.StopListening ();
			controller.StartListening ();
		}

		public override void PocketsphinxDidStartCalibration ()
		{
			Console.WriteLine ("Pocketsphinx calibration has started.");
			controller.UpdateStatus ("Pocketsphinx calibration has started");
		}

		public override void PocketsphinxDidCompleteCalibration ()
		{
			Console.WriteLine ("Pocket calibration is complete");
			controller.UpdateStatus ("Pocket calibratio is complete");
		}

		public override void PocketsphinxRecognitionLoopDidStart ()
		{
			Console.WriteLine ("Pocketsphinx is starting up");
			controller.UpdateStatus ("Pocketsphinx is starting up");
		}

		public override void PocketsphinxDidStartListening ()
		{
			Console.WriteLine ("Pocketsphinx is now listening");
			controller.UpdateStatus ("Pocketphinx is now listening");
			controller.UpdateButtonStates (true, false, false, true);
		}

		public override void PocketsphinxDidDetectSpeech ()
		{
			Console.WriteLine ("Pocketsphinx has detected speech");
			controller.UpdateStatus ("Pocketsphinx has dedected speech");
		}

		public override void PocketsphinxDidDetectFinishedSpeech ()
		{
			Console.WriteLine ("Pocketphinx has detected a second of silence, concluding utterance.");
			controller.UpdateStatus ("Pocketsphinx has dedected finished speech");
		}

		public override void PocketsphinxDidStopListening ()
		{
			Console.WriteLine ("Pocketsphinx has stopped listening");
			controller.UpdateStatus ("Pocketsphinx has stopped listening");
		}

		public override void PocketsphinxDidSuspendRecognition ()
		{
			Console.WriteLine ("Pocketsphinx has suspended recognition");
			controller.UpdateStatus ("Pocketsphinx has suspended recognition");
		}

		public override void PocketsphinxDidResumeRecognition ()
		{
			Console.WriteLine ("Pocketsphinx has resumed recognition");
			controller.UpdateStatus ("Pocketsphinx has resumed recognition");
		}

		public override void FliteDidStartSpeaking ()
		{
			Console.WriteLine ("Flite has started speaking");
			controller.UpdateStatus ("Flite has started speaking.");
		}

		public override void FliteDidFinishSpeaking ()
		{
			Console.WriteLine ("Flite has finished speaking.");
			controller.UpdateStatus ("Flite has finished speaking");
		}

		public override void PocketSphinxContinuousSetupDidFail ()
		{

		}

	}

	#endregion

}
}

