using System;
using Xamarin.Forms;

namespace NoteTaker1
{
    class NoteTaker1Page : ContentPage
    {
        public NoteTaker1Page()
        {
            this.Padding =
                new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 0);

            this.Content = new StackLayout
            {
                Children = 
                {
                    new Label 
                    { 
                        Text = "Title:" 
                    },
                    new Entry 
                    { 
                        Placeholder = "Title (optional)" 
                    },
                    new Label 
                    { 
                        Text = "Note:" 
                    },
                    new Editor
                    {
                        Keyboard = Keyboard.Create(KeyboardFlags.All),
                        BackgroundColor = Device.OnPlatform(Color.Default,
                                                            Color.Default,
                                                            Color.White),
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    new Button
                    {
                        Text = "Save",
                        HorizontalOptions = LayoutOptions.Center
                    }
                }
            };
        }
    }
}
