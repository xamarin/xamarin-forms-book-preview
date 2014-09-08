using System;
using Xamarin.Forms;

using FormsBook.Utilities;

namespace RadioColors
{
    class RadioColorsPage : ContentPage
    {
        BoxView boxView;

        public RadioColorsPage()
        {
            StackLayout radioStack = new StackLayout();

            // Three RadioButtons in the StackLayout.
            RadioButton radioButton = new RadioButton
            {
                Text = "Red",
                StyleId = "#FF0000"
            };
            radioButton.Toggled += OnRadioButtonToggled;
            radioStack.Children.Add(radioButton);

            radioButton = new RadioButton
            {
                Text = "Green",
                StyleId = "#00FF00"
            };
            radioButton.Toggled += OnRadioButtonToggled;
            radioStack.Children.Add(radioButton);

            radioButton = new RadioButton
            {
                Text = "Blue",
                StyleId = "#0000FF"
            };
            radioButton.Toggled += OnRadioButtonToggled;
            radioStack.Children.Add(radioButton);

            // A BoxView to display the selected color.
            boxView = new BoxView
            {
                VerticalOptions = LayoutOptions.FillAndExpand 
            };

            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                { 
                    new Frame
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Content = radioStack
                    },
                    boxView
                }
            };

            // Initialize.
            ((RadioButton)radioStack.Children[0]).IsToggled = true;
        }

        void OnRadioButtonToggled(object sender, ToggledEventArgs args)
        {
            if (args.Value)
            {
                // Set the color from the StyleId string.
                boxView.Color = Color.FromHex(((RadioButton)sender).StyleId);
            }
        }
    }
}
