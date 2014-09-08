using System;
using Xamarin.Forms;

namespace Greetings
{
    class GreetingsPage : ContentPage
    {
        public GreetingsPage()
        {
            this.Content = new Label
            {
                Text = "Greetings, Xamarin.Forms!",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }
    }
}
