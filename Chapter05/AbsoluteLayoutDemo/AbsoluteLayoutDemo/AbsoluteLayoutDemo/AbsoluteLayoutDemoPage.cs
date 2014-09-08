using System;
using Xamarin.Forms;

namespace AbsoluteLayoutDemo
{
    class AbsoluteLayoutDemoPage : ContentPage
    {
        AbsoluteLayout absoluteLayout;

        public AbsoluteLayoutDemoPage()
        {
            absoluteLayout = new AbsoluteLayout();

            absoluteLayout.Children.Add(new BoxView { Color = Color.Red },
                new Rectangle(0, 0, 0.33, 0.33), AbsoluteLayoutFlags.All);

            absoluteLayout.Children.Add(new BoxView { Color = Color.Green },
                new Rectangle(0.5, 0.5, 0.33, 0.33), AbsoluteLayoutFlags.All);

            absoluteLayout.Children.Add(new BoxView { Color = Color.Blue },
                new Rectangle(1, 1, 0.33, 0.33), AbsoluteLayoutFlags.All);

            absoluteLayout.HorizontalOptions = LayoutOptions.Center;
            absoluteLayout.VerticalOptions = LayoutOptions.Center;

            this.Content = absoluteLayout;

            this.SizeChanged += AbsoluteLayoutDemoPage_SizeChanged;
        }

        void AbsoluteLayoutDemoPage_SizeChanged(object sender, EventArgs e)
        {
            double dimension = Math.Min(this.Width, this.Height);
            absoluteLayout.WidthRequest = dimension;
            absoluteLayout.HeightRequest = dimension;
        }
    }
}
