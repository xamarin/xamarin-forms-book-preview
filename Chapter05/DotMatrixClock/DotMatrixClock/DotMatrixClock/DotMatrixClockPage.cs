using System;
using Xamarin.Forms;

namespace DotMatrixClock
{
    class DotMatrixClockPage : ContentPage
    {
        // 5 x 7 dot matrix patterns for 0 through 9.
        static readonly int[, ,] numberPatterns = new int[10, 7, 5]
        {
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 1, 1}, { 1, 0, 1, 0, 1}, 
                { 1, 1, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 0, 1, 0, 0}, { 0, 1, 1, 0, 0}, { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0}, 
                { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 1, 0}, 
                { 0, 0, 1, 0, 0}, { 0, 1, 0, 0, 0}, { 1, 1, 1, 1, 1}
            },
            {
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 1, 0}, { 0, 0, 1, 0, 0}, { 0, 0, 0, 1, 0},
                { 0, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 0, 0, 1, 0}, { 0, 0, 1, 1, 0}, { 0, 1, 0, 1, 0}, { 1, 0, 0, 1, 0}, 
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 1, 0}, { 0, 0, 0, 1, 0}
            },
            {
                { 1, 1, 1, 1, 1}, { 1, 0, 0, 0, 0}, { 1, 1, 1, 1, 0}, { 0, 0, 0, 0, 1}, 
                { 0, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 0, 1, 1, 0}, { 0, 1, 0, 0, 0}, { 1, 0, 0, 0, 0}, { 1, 1, 1, 1, 0}, 
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 1, 0}, { 0, 0, 1, 0, 0}, 
                { 0, 1, 0, 0, 0}, { 0, 1, 0, 0, 0}, { 0, 1, 0, 0, 0}
            },
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}, 
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 1}, 
                { 0, 0, 0, 0, 1}, { 0, 0, 0, 1, 0}, { 0, 1, 1, 0, 0}
            },
        };

        // Dot matrix pattern for a colon.
        static readonly int[,] colonPattern = new int[7, 2]
        {
                { 0, 0 }, { 1, 1 }, { 1, 1 }, { 0, 0 }, { 1, 1 }, { 1, 1 }, { 0, 0 }
        };

        // Total dots horizontally and vertically.
        const int horzDots = 41;
        const int vertDots = 7;

        // BoxView colors for on and off.
        static readonly Color colorOn = Color.Red;
        static readonly Color colorOff = new Color(0.5, 0.5, 0.5, 0.25);

        AbsoluteLayout absoluteLayout;

        // Box views for 6 digits, 7 rows, 5 columns.
        BoxView[, ,] digitBoxViews = new BoxView[6, 7, 5];

        // Box views for 2 colons, 7 rows, 2 columns.
        BoxView[, ,] colonBoxViews = new BoxView[2, 7, 2];

        public DotMatrixClockPage()
        {
            // Everything goes in the AbsoluteLayout.
            absoluteLayout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.Center
            };

            // Loop through the 6 digits in the clock.
            for (int digit = 0; digit < 6; digit++)
                for (int row = 0; row < 7; row++)
                    for (int col = 0; col < 5; col++)
                    {
                        // Create the BoxView and add to layout.
                        BoxView boxView = new BoxView();
                        digitBoxViews[digit, row, col] = boxView;
                        absoluteLayout.Children.Add(boxView);
                    }

            // Loop through the 2 colons in the clock.
            for (int colon = 0; colon < 2; colon++)
                for (int row = 0; row < 7; row++)
                    for (int col = 0; col < 2; col++)
                    {
                        // Create the BoxView and set the color.
                        BoxView boxView = new BoxView
                        {
                            Color = colonPattern[row, col] == 1 ?
                                        colorOn : colorOff
                        };
                        colonBoxViews[colon, row, col] = boxView;
                        absoluteLayout.Children.Add(boxView);
                    }

            // Set the page content to the AbsoluteLayout.
            this.Padding = new Thickness(10, 0);
            this.Content = absoluteLayout;

            // Set two event handlers.
            this.SizeChanged += OnPageSizeChanged;
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimer);

            // Initialize with a manual call to OnTimer.
            OnTimer();
        }

        void OnPageSizeChanged(object sender, EventArgs args)
        {
            double increment = (this.Width - this.Padding.HorizontalThickness) / horzDots;

            // Dot size (so 0.15 * increment for dot gap).
            double size = 0.85 * increment;

            // Begin the LayoutBounds settings.
            double x = 0;

            for (int digit = 0; digit < 6; digit++)
            {
                for (int col = 0; col < 5; col++)
                {
                    double y = 0;

                    for (int row = 0; row < 7; row++)
                    {
                        AbsoluteLayout.SetLayoutBounds(
                            digitBoxViews[digit, row, col],
                                new Rectangle(x, y, size, size));
                        y += increment;
                    }
                    x += increment;
                }
                x += increment;

                if (digit == 1 || digit == 3)
                {
                    int colon = digit / 2;

                    for (int col = 0; col < 2; col++)
                    {
                        double y = 0;

                        for (int row = 0; row < 7; row++)
                        {
                            AbsoluteLayout.SetLayoutBounds(
                                colonBoxViews[colon, row, col],
                                    new Rectangle(x, y, size, size));
                            y += increment;
                        }
                        x += increment;
                    }
                    x += increment;
                }
            }
        }

        bool OnTimer()
        {
            DateTime dateTime = DateTime.Now;

            // Convert 24-hour clock to 12-hour clock.
            int hour = (dateTime.Hour + 11) % 12 + 1;

            // Set the dot colors for each digit separately.
            SetDotMatrix(0, hour / 10);
            SetDotMatrix(1, hour % 10);
            SetDotMatrix(2, dateTime.Minute / 10);
            SetDotMatrix(3, dateTime.Minute % 10);
            SetDotMatrix(4, dateTime.Second / 10);
            SetDotMatrix(5, dateTime.Second % 10);
            return true;
        }

        void SetDotMatrix(int index, int digit)
        {
            for (int row = 0; row < 7; row++)
                for (int col = 0; col < 5; col++)
                {
                    bool isOn = numberPatterns[digit, row, col] == 1;
                    Color color = isOn ? colorOn : colorOff;
                    digitBoxViews[index, row, col].Color = color;
                }
        }
    }
}
