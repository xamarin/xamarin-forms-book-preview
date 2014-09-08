using System;
using Xamarin.Forms;

namespace DaysBetweenDates
{
    class DaysBetweenDatesPage : ContentPage
    {
        static readonly string dateFormat = "D";
        DatePicker fromDatePicker, toDatePicker;
        Label resultLabel;

        public DaysBetweenDatesPage()
        {
            // Create DatePicker views.
            fromDatePicker = new DatePicker
            {
                Format = dateFormat,
                HorizontalOptions = LayoutOptions.Center
            };
            fromDatePicker.DateSelected += OnDateSelected;

            toDatePicker = new DatePicker
            {
                Format = dateFormat,
                HorizontalOptions = LayoutOptions.Center
            };
            toDatePicker.DateSelected += OnDateSelected;

            // Create Label for displaying result.
            resultLabel = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    // Program title and underline.
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = 
                        {
                            new Label
                            {
                                Text = "Days Between Dates",
                                Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold),
                                TextColor = Color.Accent
                            },
                            new BoxView
                            {
                                HeightRequest = 3,
                                Color = Color.Accent
                            }
                        }
                    },

                    // "From" DatePicker view.
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Children = 
                        {
                            new Label
                            {
                                Text = "From:",
                                HorizontalOptions = LayoutOptions.Center
                            },

                            fromDatePicker
                        }
                    },

                    // "To" DatePicker view.
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Children = 
                        {
                            new Label
                            {
                                Text = "To:",
                                HorizontalOptions = LayoutOptions.Center
                            },

                            toDatePicker
                        }
                    },

                    // Label for result.
                    resultLabel
                }
            };

            // Initialize results display.
            OnDateSelected(null, null);
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            int days = (int)Math.Round((toDatePicker.Date - fromDatePicker.Date).TotalDays);
            resultLabel.Text = String.Format("{0:F0} day{1} between dates",
                                                days, days == 1 ? "" : "s");
        }
    }
}
