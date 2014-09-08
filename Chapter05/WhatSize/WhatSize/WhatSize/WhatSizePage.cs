using System;
using Xamarin.Forms;

namespace WhatSize
{
    class WhatSizePage : ContentPage
    {
        Label label;

        public WhatSizePage()
        {
            label = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            this.Content = label;
            this.SizeChanged += OnPageSizeChanged;
        }

        void OnPageSizeChanged(object sender, EventArgs args)
        {
            label.Text = String.Format("{0} \u00D7 {1}", this.Width, this.Height);
        }
    }
}
