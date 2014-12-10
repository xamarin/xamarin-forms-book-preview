using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace PlatformVisuals.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			Forms.Init ();
			LoadApplication (new App ());
			return base.FinishedLaunching (app, options);
        }
    }
}
