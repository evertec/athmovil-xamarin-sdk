using System;
using System.Globalization;
using Xamarin.Forms;

namespace TestApp.Converters
{
    public struct QuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => $"x{value}";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
