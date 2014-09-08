using System;
using System.Reflection;
using Xamarin.Forms;

namespace ReflectedColors
{
    class ReflectedColorsPage : ContentPage
    {
        public ReflectedColorsPage()
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
                        CreateColorLabel((Color) fieldInfo.GetValue(null), 
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
                        CreateColorLabel((Color) propInfo.GetValue(null), 
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

        Label CreateColorLabel(Color color, string name)
        {
            Color backgroundColor = Color.Default;

            if (color != Color.Default)
            {
                // Standard luminance calculation
                double luminance = 0.30 * color.R +
                                   0.59 * color.G +
                                   0.11 * color.B;

                backgroundColor = 
                    luminance > 0.5 ? Color.Black : Color.White;
            }

            // Create the Label.
            return new Label
            {
                Text = name,
                TextColor = color,
                BackgroundColor = backgroundColor, 
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };
        }
    }
}

