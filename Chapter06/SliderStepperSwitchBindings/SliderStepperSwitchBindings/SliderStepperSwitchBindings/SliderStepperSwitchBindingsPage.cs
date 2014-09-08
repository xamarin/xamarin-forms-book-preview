using System;
using Xamarin.Forms;

namespace SliderStepperSwitchBindings
{
    class SliderStepperSwitchBindingsPage : ContentPage
    {
        public SliderStepperSwitchBindingsPage()
        {
            // Create a Slider.
            Slider slider = new Slider
            {
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            // Create a Label to display the Slider value.
            Label sliderValueLabel = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a Stepper.
            Stepper stepper = new Stepper
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a Label to display the Stepper value.
            Label stepperValueLabel = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center,
            };

            // Create a Switch.
            Switch switcher = new Switch
            {
                IsToggled = true,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a Label to display the Switch value.
            Label switchToggledLabel = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            // Put them all in a StackLayout.
            this.Content = new StackLayout
            {
                Children = 
                {
                    slider,
                    sliderValueLabel,
                    stepper,
                    stepperValueLabel,
                    switcher,
                    switchToggledLabel,  
                }
            };

            sliderValueLabel.BindingContext = slider;
            sliderValueLabel.SetBinding (Label.OpacityProperty, "Value");

            slider.BindingContext = switcher;
            slider.SetBinding (Slider.IsEnabledProperty, "IsToggled");

            stepper.BindingContext = switcher;
            stepper.SetBinding (Stepper.IsEnabledProperty, "IsToggled");

            sliderValueLabel.SetBinding (Label.TextProperty, 
                new Binding ("Value", BindingMode.Default, null, null,
                                "The Slider value is {0:F2}"));

            stepperValueLabel.BindingContext = stepper;
            stepperValueLabel.SetBinding (Label.TextProperty,
                new Binding ("Value", BindingMode.Default, null, null,
                                "The Stepper value is {0}"));
                    
            switchToggledLabel.BindingContext = switcher;
            switchToggledLabel.SetBinding (Label.TextProperty,
                new Binding ("IsToggled", BindingMode.Default, null, null,
                                "The Switch is toggled {0}"));
        }
    }
}
