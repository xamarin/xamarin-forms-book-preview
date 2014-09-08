using System;

namespace PlatformHelpers
{
    public interface ILifecycleHelper
    {
        event Action Suspending;

        event Action Resuming;
    }
}
