using System;
using Xamarin.Forms;

namespace NoteTaker7
{
    class HomePage : ContentPage
    {
        public HomePage()
        {
            this.Title = "Note Taker 7";

            Button button = new Button
            {
                Text = "Add New Note",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            button.Clicked += (sender, args) =>
                {
                    // Create unique filename.
                    DateTime datetime = DateTime.UtcNow;
                    string filename = datetime.ToString("yyyyMMddHHmmssfff.note");

                    // Navigate to new NotePage.
                    this.Navigation.PushAsync(new NotePage(filename));
                };

            this.Content = button;
        }
    }
}
