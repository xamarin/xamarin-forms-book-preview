using System;
using Xamarin.Forms;

namespace BitmapFromWebsite
{
    class BitmapFromWebsitePage : ContentPage
    {
        public BitmapFromWebsitePage()
        {
            string uri = "http://developer.xamarin.com/demo/IMG_1415.JPG";

            this.Content = new Image
            {
                Source = ImageSource.FromUri(new Uri(uri))
            };
        }
    }
}
