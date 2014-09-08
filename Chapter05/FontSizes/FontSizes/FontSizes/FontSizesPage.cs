using System;
using Xamarin.Forms;

namespace FontSizes
{
    class FontSizesPage : ContentPage
    {
        public FontSizesPage()
        {
            BackgroundColor = Color.White;

            StackLayout stackLayout = new StackLayout();
 
            // Add NamedSize fonts to StackLayout.
            foreach (NamedSize namedSize in Enum.GetValues(typeof(NamedSize)))
            {
                stackLayout.Children.Add(
                    new Label
                    {
                        Text = String.Format("System font of named size {0}",
                                             namedSize),
                        Font = Font.SystemFontOfSize(namedSize),
                        TextColor = Color.Black
                    });
            }
 
            // Resolution in device-independent units per inch.
            double resolution = Device.OnPlatform(160, 160, 240);
    
            // Draw horizontal separator line.
            stackLayout.Children.Add(
                new BoxView
                {
                    Color = Color.Accent,
                    HeightRequest = resolution / 80
                });
 
            // Add numeric sized fonts to StackLayout.
            int[] ptSizes = { 4, 6, 8, 10, 12 };

            foreach (double ptSize in ptSizes)
            {
                double fontSize = resolution * ptSize / 72;
 
                stackLayout.Children.Add(
                    new Label
                    {
                        Text = String.Format("System font of point size " +
                                             "{0} and font size {1:F1}",
                                             ptSize, fontSize),
                        Font = Font.SystemFontOfSize(fontSize),
                        TextColor = Color.Black
                    });
            }
 
            this.Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
            this.Content = stackLayout;
        }
    }
}
