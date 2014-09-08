using System;
using Xamarin.Forms;

namespace KeypadGrid
{
    class KeypadGridPage : ContentPage
    {
        Label displayLabel;
        Button backspaceButton;

        public KeypadGridPage()
        {
            // Create a centered grid for the entire keypad.
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            // First row with Label and backspace Button.
            displayLabel = new Label
            {
                Text = "0",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.Center
            };
            grid.Children.Add(displayLabel, 0, 2, 0, 1);

            backspaceButton = new Button
            {
                Text = "\u21E6",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                IsEnabled = false
            };
            backspaceButton.Clicked += OnBackspaceButtonClicked;
            grid.Children.Add(backspaceButton, 2, 0);

            // Now do the 10 number keys.
            for (int num = 0; num < 10; num++)
            {
                Button numberButton = new Button
                {
                    Text = num.ToString(),
                    Font = Font.SystemFontOfSize(NamedSize.Large),
                    StyleId = num.ToString(),
                    BorderWidth = 1
                };
                numberButton.Clicked += OnNumberButtonClicked;

                int left = (num == 0) ? 0 : (num + 2) % 3;
                int right = (num == 0) ? 3 : left + 1;
                int top = (12 - num) / 3;
                int bottom = top + 1;
                grid.Children.Add(numberButton, left, right, top, bottom);
            }

            this.Content = grid;
        }

        void OnNumberButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            displayLabel.Text += (string)button.StyleId;
            backspaceButton.IsEnabled = true;
        }

        void OnBackspaceButtonClicked(object sender, EventArgs args)
        {
            string text = displayLabel.Text;
            displayLabel.Text = text.Substring(0, text.Length - 1);
            backspaceButton.IsEnabled = displayLabel.Text.Length > 0;
        }
    }
}
