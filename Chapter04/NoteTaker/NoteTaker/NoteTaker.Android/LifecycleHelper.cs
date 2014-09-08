using System;
using Xamarin.Forms;

//-------------------------------------------------------
// This Android LifecycleHelper class requies calls from 
// OnPause and OnResume overrides in MainActivity to the 
// static OnPause and OnResume in this class!
//-------------------------------------------------------

[assembly: Dependency(typeof(PlatformHelpers.Droid.LifecycleHelper))]

namespace PlatformHelpers.Droid
{
    class LifecycleHelper : ILifecycleHelper
    {
        public event Action Suspending;

        public event Action Resuming;

        
        static LifecycleHelper instance;

        public LifecycleHelper()
        {
            instance = this;
        }

        public static void OnPause()
        {
            if (instance != null && instance.Suspending != null)
                instance.Suspending();
        }

        public static void OnResume()
        {
            if (instance != null && instance.Resuming != null)
                instance.Resuming();
        }
    }
}
