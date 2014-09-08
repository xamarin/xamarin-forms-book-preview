using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;

namespace NoteTaker.Droid
{
    [Activity(Label = "Note Taker", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            SetPage(App.GetMainPage());
        }

        protected override void OnPause()
        {
            PlatformHelpers.Droid.LifecycleHelper.OnPause();
            base.OnPause();
        }

        protected override void OnResume()
        {
            PlatformHelpers.Droid.LifecycleHelper.OnResume();
            base.OnResume();
        }
    }
}

