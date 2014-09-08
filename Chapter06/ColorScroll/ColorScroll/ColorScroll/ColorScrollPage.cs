using System;
using Xamarin.Forms;

using FormsBook.Utilities;

namespace ColorScroll
{
    class ColorScrollPage : ContentPage
    {
        Grid mainGrid;
        StackLayout controllersStack;
        BoxView boxView;
        Label[] labels = new Label[3];
        Slider[] sliders = new Slider[3];

        enum ColorMode
        {
            RgbHex,
            RgbFloat,
            Hsl
        }

        ColorMode currentColorMode = ColorMode.RgbHex;
        Color currentColor = Color.Aqua;
        bool ignoreSliderValueChanges;

        string[] rgbHexFormat = { "Red = {0:X2}", "Green = {0:X2}", "Blue = {0:X2}" };
        string[] rgbFloatFormat = { "Red = {0:F2}", "Green = {0:F2}", "Blue = {0:F2}" };
        string[] hslFormat = { "Hue = {0:F2}", "Saturation = {0:F2}", "Luminosity = {0:F2}" };

        public ColorScrollPage()
        {
            // Define a 2 x 2 Grid that will be modified for portrait
            //      or landscape in SizeChanged handler.
            mainGrid = new Grid
            {
                ColumnDefinitions = 
                {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
                RowDefinitions = 
                {
                    new RowDefinition(),
                    new RowDefinition()
                }
            };

            // Put all Labels and Sliders in StackLayout.
            //  Wait until SizeChanged to set row and column.
            controllersStack = new StackLayout();
            mainGrid.Children.Add(controllersStack);

            // Also wait until SizeChanged to set BoxView row and column.
            boxView = new BoxView 
            {
                Color = currentColor
            };
            mainGrid.Children.Add(boxView);

            // Assemble 3-column Grid to display color options.
            Grid radioGrid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            string[] modeLabels = { "Hex RGB", "Float RGB", "HSL" };

            foreach (ColorMode colorMode in Enum.GetValues(typeof(ColorMode)))
            {
                // Define a Label to be used as a radio button.
                Label radioLabel = new Label
                {
                    Text = modeLabels[(int)colorMode],
                    StyleId = colorMode.ToString(),
                    XAlign = TextAlignment.Center,
                    Opacity = 0.5
                };
                radioLabel.AddRadioToggler(OnRadioLabelToggled);

                // Set default item.
                radioLabel.SetRadioState(colorMode == ColorMode.RgbHex);

                // Add it to the Grid.
                radioGrid.Children.AddHorizontal(radioLabel);

                // Set the column width to "star" to equally space them.
                radioGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
            }
            controllersStack.Children.Add(radioGrid);

            // Create Labels and Sliders for the three color components.
            for (int component = 0; component < 3; component++)
            {
                labels[component] = new Label
                {
                    XAlign = TextAlignment.Center
                };
                controllersStack.Children.Add(labels[component]);

                // Set same ValueChanged handler for all sliders.
                sliders[component] = new Slider();
                sliders[component].ValueChanged += OnSliderValueChanged;
                controllersStack.Children.Add(sliders[component]);
            }

            // Build page.
            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.Content = mainGrid;

            // Set SizeChanged handler.
            SizeChanged += OnPageSizeChanged;

            // Initialize Slider values.
            SetSlidersAndBindings ();
        }

        void OnPageSizeChanged(object sender, EventArgs args)
        {
            // Portrait mode.
            if (this.Height > this.Width)
            {
                // Adjust column and row definitions.
                mainGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                mainGrid.ColumnDefinitions[1].Width = new GridLength(0);
                mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                mainGrid.RowDefinitions[1].Height = GridLength.Auto;

                // Set rows and columns of BoxView and StackLayout.
                Grid.SetColumn(boxView, 0);
                Grid.SetRow(boxView, 0);

                Grid.SetColumn(controllersStack, 0);
                Grid.SetRow(controllersStack, 1);
            }
            // Landscape mode.
            else
            {
                // Adjust column and row definitions.
                mainGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                mainGrid.RowDefinitions[1].Height = new GridLength(0);

                // Set rows and columns of BoxView and StackLayout.
                Grid.SetColumn(boxView, 0);
                Grid.SetRow(boxView, 0);

                Grid.SetColumn(controllersStack, 1);
                Grid.SetRow(controllersStack, 0);
            }
        }

        void OnRadioLabelToggled(View view)
        {
            // Set Opacity to indicate toggled state.
            view.Opacity = view.GetRadioState() ? 1.0 : 0.5;

            // Possibly change the current color mode.
            if (view.GetRadioState ())
            {
                ColorMode newColorMode = 
                    (ColorMode) Enum.Parse (typeof(ColorMode), view.StyleId);

                if (currentColorMode != newColorMode)
                {
                    currentColorMode = newColorMode;
                    SetSlidersAndBindings ();
                }
            }
        }

        void SetSlidersAndBindings()
        {
            // Don't handle ValueChange events from Sliders!
            ignoreSliderValueChanges = true;

            double sliderMax = 1;
            string[] format = null;
            IValueConverter valueConverter = null;

            // Set local variables for color mode.
            switch (currentColorMode)
            {
                case ColorMode.RgbHex:
                    sliderMax = 255;
                    format = rgbHexFormat;
                    valueConverter = new DoubleToIntegerConverter();
                    break;

                case ColorMode.RgbFloat:
                    format = rgbFloatFormat;
                    break;

                case ColorMode.Hsl:
                    format = hslFormat;
                    break;
            }

            // Bind the labels to the sliders.
            for (int i = 0; i < 3; i++)
            {
                sliders[i].Maximum = sliderMax;
                labels[i].BindingContext = sliders[i];
                labels[i].SetBinding(Label.TextProperty,
                    new Binding("Value", BindingMode.OneWay, valueConverter, null, format[i]));
            }

            // Now set the slider values.
            Color currentColor = this.currentColor;

            switch (currentColorMode)
            {
                case ColorMode.RgbHex:
                    sliders[0].Value = 255 * currentColor.R;
                    sliders[1].Value = 255 * currentColor.G;
                    sliders[2].Value = 255 * currentColor.B;
                    break;

                case ColorMode.RgbFloat:
                    sliders[0].Value = currentColor.R;
                    sliders[1].Value = currentColor.G;
                    sliders[2].Value = currentColor.B;
                    break;

                case ColorMode.Hsl:
                    sliders[0].Value = currentColor.Hue;
                    sliders[1].Value = currentColor.Saturation;
                    sliders[2].Value = currentColor.Luminosity;
                    break;
            }

            // Resume handling ValueChange events.
            ignoreSliderValueChanges = false;

            // Sync up the colors and sliders. 
            SetColorFromSliders();
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (ignoreSliderValueChanges)
                return;

            SetColorFromSliders();
        }

        void SetColorFromSliders()
        {
            double d0 = sliders[0].Value;
            double d1 = sliders[1].Value;
            double d2 = sliders[2].Value;

            switch (currentColorMode)
            {
                case ColorMode.RgbHex:
                    currentColor = Color.FromRgb((int)d0, (int)d1, (int)d2);
                    break;

                case ColorMode.RgbFloat:
                    currentColor = Color.FromRgb(d0, d1, d2);
                    break;

                case ColorMode.Hsl:
                    currentColor = Color.FromHsla(d0, d1, d2);
                    break;
            }
            boxView.Color = currentColor;
        }
    }
}
