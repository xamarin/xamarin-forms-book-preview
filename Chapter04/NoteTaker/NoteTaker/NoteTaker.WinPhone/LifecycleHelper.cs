using Microsoft.Phone.Shell;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformHelpers.WinPhone.LifecycleHelper))]

namespace PlatformHelpers.WinPhone
{
    class LifecycleHelper : ILifecycleHelper
    {
        public event Action Suspending;

        public event Action Resuming;

        public LifecycleHelper()
        {
            PhoneApplicationService.Current.Launching +=
                (sender, args) =>
                {
                    if (Resuming != null)
                        Resuming();
                };

            PhoneApplicationService.Current.Activated +=
                (sender, args) =>
                {
                    if (Resuming != null)
                        Resuming();
                };

            PhoneApplicationService.Current.Deactivated +=
                (sender, args) =>
                {   
                    if (Suspending != null)
                        Suspending();
                };

            PhoneApplicationService.Current.Closing +=
                (sender, args) =>
                {
                    if (Suspending != null)
                        Suspending();
                };
        }
    }
}
