using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Investor.UI.Windows
{
    [ValueConversion(typeof(double), typeof(GridLength))]
    public class DoubleToGridLengthConverter : BaseValueConverter<DoubleToGridLengthConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new GridLength((double)value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
