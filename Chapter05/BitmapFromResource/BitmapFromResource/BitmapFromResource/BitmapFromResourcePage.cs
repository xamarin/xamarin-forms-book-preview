using System;
using Xamarin.Forms;

namespace BitmapFromResource
{
    class BitmapFromResourcePage : ContentPage
    {
        public BitmapFromResourcePage()
        {
            string resource = "BitmapFromResource.Images.ModernUserInterface256.jpg";

            this.Content = new Image
            {
                Source = ImageSource.FromResource(resource),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }
    }
}
