using System;
using System.Globalization;

namespace  NuevoCheckout.Converters
{
    public struct PriceConverter : IValueConverter
    {
        public string Convert(double price)
        {
            return  (string)Convert(price, price.GetType(), null, CultureInfo.CurrentCulture);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => String.Format("{0:C}", value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
