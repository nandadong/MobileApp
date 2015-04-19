using NUnit.Framework;
using System;
using HomeAutomationApp;

namespace UI_test_addDev
{
	[TestFixture ()]
	public class CrossPlatformTests
	{
		protected IApp app;

		//static readonly Func<AppQuery, AppQuery> InitialMessage = c => c.Marked("MyLabel").Text("Hello, Xamarin.Forms!");
		static readonly Func<AppQuery, AppQuery> Button = c => c.Marked("MyButton");
		//static readonly Func<AppQuery, AppQuery> DoneMessage = c => c.Marked("MyLabel").Text("Was clicked");

		/// <summary>
		/// This test checks the value of a Label, presses a Button, then checks the Label text again
		/// </summary>
		[Test ()]
		public void TestCase ()
		{
			// Arrange
			AppResult[] result = app.Query(InitialMessage);
			Assert.IsTrue(result.Any(), "The initial message string isn't correct - maybe the app wasn't re-started?");

			// Act
			app.Tap(Button);

			// Assert
			result = app.Query(DoneMessage);
			Assert.IsTrue(result.Any(), "The 'clicked' message is not being displayed.");
		}
	}
}

