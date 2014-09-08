using System;
using Xamarin.Forms;

namespace ColorList
{
    class ColorListPage : ContentPage
    {
        public ColorListPage()
        {
            this.Padding = 
                new Thickness (5, Device.OnPlatform (20, 5, 5), 5, 5);

            this.Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "White",
                        TextColor = Color.White
                    },
                    new Label
                    {
                        Text = "Silver",
                        TextColor = Color.Silver
                    },
                    new Label
                    {
                        Text = "Gray",
                        TextColor = Color.Gray
                    },
                    new Label
                    {
                        Text = "Black",
                        TextColor = Color.Black
                    },
                    new Label
                    {
                        Text = "Red", 
                        TextColor = Color.Red
                    },
                    new Label
                    {
                        Text = "Maroon",
                        TextColor = Color.Maroon
                    },
                    new Label
                    {
                        Text = "Yellow",
                        TextColor = Color.Yellow
                    },
                    new Label
                    {
                        Text = "Olive",
                        TextColor = Color.Olive
                    },
                    new Label
                    {
                        Text = "Lime",
                        TextColor = Color.Lime
                    },
                    new Label
                    {
                        Text = "Green",
                        TextColor = Color.Green
                    },
                    new Label
                    {
                        Text = "Aqua",
                        TextColor = Color.Aqua
                    },
                    new Label
                    {
                        Text = "Teal",
                        TextColor = Color.Teal
                    },
                    new Label
                    {
                        Text = "Blue",
                        TextColor = Color.Blue
                    },
                    new Label
                    {
                        Text = "Navy",
                        TextColor = Color.Navy
                    },
                    new Label
                    {
                        Text = "Fuschia",
                        TextColor = Color.Fuschia
                    },
                    new Label
                    {
                        Text = "Purple",
                        TextColor = Color.Purple
                    }
                }
            };
        }
    }
}
