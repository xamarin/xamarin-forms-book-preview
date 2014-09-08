using System;
using Xamarin.Forms;

namespace DigitalClock
{
    class DigitalClockPage : ContentPage
    {
        Label clockLabel;

        public DigitalClockPage()
        {
            // Create the centered Label for the clock display.
            clockLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            this.Content = clockLabel;

            // Set a SizeChanged event on the page.
            this.SizeChanged += OnPageSizeChanged;

            // Start the timer going.
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
        }

        void OnPageSizeChanged(object sender, EventArgs args)
        {
            // Scale the font size to the page width
            //      (based on 11 characters in the displayed string).
            if (this.Width > 0)
                clockLabel.Font = Font.SystemFontOfSize(this.Width / 6);
        }

        bool OnTimerTick()
        {
            // Set the Text property of the Label.
            clockLabel.Text = DateTime.Now.ToString("h:mm:ss tt");
            return true;
        }
    }
}
