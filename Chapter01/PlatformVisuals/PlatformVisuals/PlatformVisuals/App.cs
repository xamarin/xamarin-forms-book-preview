using System;
using Xamarin.Forms;

namespace PlatformVisuals
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new NavigationPage(new PlatformVisualsPage());
        }
    }
}
