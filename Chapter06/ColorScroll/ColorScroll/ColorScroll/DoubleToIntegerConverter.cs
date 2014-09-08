using System;
using System.Globalization;
using Xamarin.Forms;

namespace FormsBook.Utilities
{
    public class DoubleToIntegerConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, 
                               object parameter, CultureInfo culture)
        {
            return (int)(double)value;
        }

        public object ConvertBack (object value, Type targetType, 
                                   object parameter, CultureInfo culture)
        {
            return (double)(int)value;
        }
    }
}

