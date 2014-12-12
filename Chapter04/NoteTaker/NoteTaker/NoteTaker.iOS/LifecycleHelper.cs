using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformHelpers.iOS.LifecycleHelper))]

namespace PlatformHelpers.iOS
{
    class LifecycleHelper : ILifecycleHelper
    {
        public event Action Suspending;

        public event Action Resuming;

        public LifecycleHelper()
        {
            UIApplication.Notifications.ObserveDidEnterBackground(
                (sender, args) =>
                {
                    if (Suspending != null)
                        Suspending();
                });

            UIApplication.Notifications.ObserveWillEnterForeground(
                (sender, args) =>
                {
                    if (Resuming != null)
                        Resuming();
                });
        }
    }
}