using System;
using Xamarin.Forms;

namespace WhatSizeBindings
{
    class WhatSizeBindingsPage : ContentPage
    {
        public WhatSizeBindingsPage()
        {
            Font font = Font.SystemFontOfSize(NamedSize.Large); 

            // Create Label views for displaying page width and height.
            Label widthLabel = new Label
            {
                Font = font
            };

            Label heightLabel = new Label 
            {
                Font = font
            };

            // Define bindings on Labels.
            widthLabel.BindingContext = this;
            widthLabel.SetBinding (Label.TextProperty, 
                Binding.Create<ContentPage> (src => src.Width, 
                                             BindingMode.OneWay, 
                                             null, null, "{0:F0}"));

            heightLabel.BindingContext = this;
            heightLabel.SetBinding (Label.TextProperty, 
                new Binding ("Height", 
                             BindingMode.OneWay, 
                             null, null, "{0:F0}"));
                
            // Assemble a horizontal StackLayout with the three labels.
            this.Content = new StackLayout 
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = 
                {
                    widthLabel,
                    new Label 
                    {
                        Text = " x ",
                        Font = font
                    },
                    heightLabel
                }
            };
        }
    }
}

