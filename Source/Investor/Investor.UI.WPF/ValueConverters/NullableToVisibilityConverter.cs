using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Investor.UI.WPF
{
    /// <summary>
    /// A value converter that converts nullable values to visibility.
    /// Basically, <see langword="not null"/> to <see cref="Visibility.Visible"/>, and <see langword="null"/> to <see cref="Visibility.Collapsed"/>.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class NullableToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string revert && revert == "revert")
            {
                return value is null ? Visibility.Visible : Visibility.Collapsed;
            }
            return value is not null ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

