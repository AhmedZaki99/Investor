using System;
using System.Globalization;
using System.Windows.Data;

namespace Investor.UI.Windows
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolToOppositeConverter : BaseValueConverter<BoolToOppositeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
