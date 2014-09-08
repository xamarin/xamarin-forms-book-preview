using System;
using Xamarin.Forms;

namespace FormsBook.Utilities
{
    class RadioButton : ContentView
    {
        static readonly string checkOff = "\x25CB";
        static readonly string checkOn = "\u25C9";
        Label checkLabel;

        public RadioButton()
        {
            // Assemble the view.
            checkLabel = new Label
            {
                Text = checkOff
            };

            Label textLabel = new Label();

            this.Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    checkLabel,
                    textLabel
                }
            };

            // Set the bindings.
            checkLabel.BindingContext = this;
            checkLabel.SetBinding(Label.FontProperty, "Font");

            textLabel.BindingContext = this;
            textLabel.SetBinding(Label.TextProperty, "Text");
            textLabel.SetBinding(Label.FontProperty, "Font");

            // Install a Tap recognizer.
            TapGestureRecognizer recognizer = new TapGestureRecognizer
            {
                Parent = this,
                NumberOfTapsRequired = 1,
                TappedCallback = (View view, Object args) =>
                    {
                        ((RadioButton)view).IsToggled = true;
                    }
            };

            this.GestureRecognizers.Add (recognizer);
        }

        // Define the Text bindable property and property.
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text",                 // property name
                                    typeof(string),         // property type
                                    typeof(RadioButton),    // this type
                                    null);                  // initial value

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        // Define the Font bindable property and property.
        public static readonly BindableProperty FontProperty = 
            BindableProperty.Create<RadioButton, Font> 
                (radio => radio.Font,                       // property
                    Font.SystemFontOfSize(NamedSize.Large));   // initial value

        public Font Font
        {
            set { SetValue(FontProperty, value); }
            get { return (Font)GetValue(FontProperty); }
        }

        // Define the Toggled event, IsToggled bindable property and property.
        public event EventHandler<ToggledEventArgs> Toggled;

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create("IsToggled", 
                                    typeof(bool),           // property type 
                                    typeof(RadioButton),    // this type
                                    false,                  // initial value
                                    BindingMode.TwoWay,     // default binding
                                    null,                   // validation
                                    OnIsToggledPropertyChanged);

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        // Property changed handler for the IsToggled property.
        static void OnIsToggledPropertyChanged(BindableObject sender, 
                                                object oldValue, object newValue)
        {
            // Get the object whose property is changing.
            RadioButton radioButton = (RadioButton)sender;
            radioButton.OnIsToggledPropertyChanged((bool)oldValue, (bool)newValue);
        }

        void OnIsToggledPropertyChanged(bool oldValue, bool newValue)
        {
            this.checkLabel.Text = newValue ? checkOn : checkOff;

            // Fire the event.
            if (this.Toggled != null)
            {
                this.Toggled(this, new ToggledEventArgs(this.IsToggled));
            }

            // If toggled on, toggle off all siblings.
            if (this.IsToggled)
            {
                Layout<View> parent = this.ParentView as Layout<View>;

                if (parent != null)
                {
                    foreach (View view in parent.Children)
                    {
                        if (view is RadioButton && view != this)
                        {
                            ((RadioButton)view).IsToggled = false;
                        }
                    }
                }
            }
        }
    }
}
