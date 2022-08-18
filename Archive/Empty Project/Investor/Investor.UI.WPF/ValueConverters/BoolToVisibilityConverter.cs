using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Investor.UI.WPF
{
    /// <summary>
    /// A value converter that converts boolean values to visibility.
    /// Basically, <see langword="true"/> to <see cref="Visibility.Visible"/>, and <see langword="false"/> to <see cref="Visibility.Collapsed"/>.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool condition = (bool)value;
            var falseVisibility = Visibility.Collapsed;

            if (parameter is string flags)
            {
                if (flags.Contains("revert"))
                {
                    condition = !condition;
                }
                if (flags.Contains("hide"))
                {
                    falseVisibility = Visibility.Hidden;
                }
            }
            return condition ? Visibility.Visible : falseVisibility;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string flags && flags.Contains("revert"))
            {
                return (Visibility)value != Visibility.Visible;
            }
            return (Visibility)value == Visibility.Visible;
        }
    }
}

