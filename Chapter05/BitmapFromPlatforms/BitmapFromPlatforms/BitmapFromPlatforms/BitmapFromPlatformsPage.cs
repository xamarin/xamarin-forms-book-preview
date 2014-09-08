using System;
using Xamarin.Forms;

namespace BitmapFromPlatforms
{
    class BitmapFromPlatformsPage : ContentPage
    {
        public BitmapFromPlatformsPage()
        {
            this.Content = new Image
            {
                Source = ImageSource.FromFile(
                            Device.OnPlatform("Icon.png", 
                                              "Icon.png", 
                                              "Assets/ApplicationIcon.png"))
            };
        }
    }
}
