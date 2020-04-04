using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Investor.UI.Core;

namespace Investor.UI.Windows
{
    /// <summary>
    /// A converter that takes in a UI color from the core and converts it to the view brush in this app.
    /// </summary>
    public class UIColorToDesignBrushConverter : BaseValueConverter<UIColorToDesignBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((UIColors)value)
            {
                case UIColors.LightModeMainTheme:
                    return (SolidColorBrush)Application.Current.FindResource("LightMode.MainTheme.Brush");

                case UIColors.DarkModeMainTheme:
                    return (SolidColorBrush)Application.Current.FindResource("DarkMode.MainTheme.Brush");

                default:
                    System.Diagnostics.Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
