using System;
using System.Reflection;
using Xamarin.Forms;

namespace ColorBlocks
{
    class ColorBlocksPage : ContentPage
    {
        public ColorBlocksPage()
        {
            StackLayout stackLayout = new StackLayout();

            // Loop through the Color structure fields.
            foreach (FieldInfo fieldInfo in
                            typeof(Color).GetRuntimeFields())
            {
                if (fieldInfo.IsPublic &&
                    fieldInfo.IsStatic &&
                    fieldInfo.FieldType == typeof(Color))
                {
                    stackLayout.Children.Add(
                        CreateColorView((Color)fieldInfo.GetValue(null),
                                        fieldInfo.Name));
                }
            }

            // Loop through the Color structure properties.
            foreach (PropertyInfo propInfo in
                            typeof(Color).GetRuntimeProperties())
            {
                MethodInfo methodInfo = propInfo.GetMethod;

                if (methodInfo.IsPublic &&
                    methodInfo.IsStatic &&
                    methodInfo.ReturnType == typeof(Color))
                {
                    stackLayout.Children.Add(
                        CreateColorView((Color)propInfo.GetValue(null),
                                        propInfo.Name));
                }
            }

            this.Padding =
                new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);

            // Put the StackLayout in a ScrollView.
            this.Content = new ScrollView
            {
                Content = stackLayout
            };
        }

        View CreateColorView(Color color, string name)
        {
            return new Frame
            {
                OutlineColor = Color.Accent,
                Padding = new Thickness(5),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                    Children = 
                    {
                        new BoxView
                        {
                            Color = color
                        },
                        new Label
                        {
                            Text = name,
                            Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                            VerticalOptions = LayoutOptions.Center
                        },
                        new Label
                        {
                            Text = String.Format("{0:X2}-{1:X2}-{2:X2}",
                                                 (int)(255 * color.R),
                                                 (int)(255 * color.G),
                                                 (int)(255 * color.B)),
                            VerticalOptions = LayoutOptions.Center,
                            IsVisible = color != Color.Default
                        }
                    }
                }
            };
        }
    }
}

