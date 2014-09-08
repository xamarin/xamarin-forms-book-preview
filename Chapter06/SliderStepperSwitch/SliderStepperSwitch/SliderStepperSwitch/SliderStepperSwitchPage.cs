using System;
using Xamarin.Forms;

namespace SliderStepperSwitch
{
    class SliderStepperSwitchPage : ContentPage
    {
        Label sliderValueLabel, stepperValueLabel, switchToggledLabel;
        
        public SliderStepperSwitchPage()
        {
            // Create a Slider and set event handler.
            Slider slider = new Slider
            {
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            slider.ValueChanged += OnSliderValueChanged;

            // Create a Label to display the Slider value.
            sliderValueLabel = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a Stepper and set event handler.
            Stepper stepper = new Stepper
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            stepper.ValueChanged += 
                (object sender, ValueChangedEventArgs args) =>
                {
                    stepperValueLabel.Text = args.NewValue.ToString();
                };
                
            // Create a Label to display the Stepper value.
            stepperValueLabel = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a Switch and set event handler.
            Switch switcher = new Switch
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            switcher.Toggled += OnSwitcherToggled;

            // Create a Label to display the Switch value.
            switchToggledLabel = new Label
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
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            sliderValueLabel.Text = args.NewValue.ToString();
        }

        void OnSwitcherToggled(object sender, ToggledEventArgs args)
        {
            Switch switcher = (Switch)sender;
            switchToggledLabel.Text = switcher.IsToggled.ToString();
        }
    }
}
