using System.Windows;
using System.Windows.Media;

namespace Investor.UI.Windows
{
    public static class WindowDesign
    {

        public static readonly DependencyProperty CaptionColorProperty =
            DependencyProperty.RegisterAttached("CaptionColor", typeof(SolidColorBrush), typeof(WindowDesign));

        public static SolidColorBrush GetCaptionColor(Window element)
        {
            return (SolidColorBrush)element.GetValue(CaptionColorProperty);
        }

        public static void SetCaptionColor(Window element, SolidColorBrush value)
        {
            element.SetValue(CaptionColorProperty, value);
        }



        public static readonly DependencyProperty CaptionBorderColorProperty =
            DependencyProperty.RegisterAttached("CaptionBorderColor", typeof(SolidColorBrush), typeof(WindowDesign));

        public static SolidColorBrush GetCaptionBorderColor(Window element)
        {
            return (SolidColorBrush)element.GetValue(CaptionBorderColorProperty);
        }

        public static void SetCaptionBorderColor(Window element, SolidColorBrush value)
        {
            element.SetValue(CaptionBorderColorProperty, value);
        }



        public static readonly DependencyProperty CaptionBorderThicknessProperty =
            DependencyProperty.RegisterAttached("CaptionBorderThickness", typeof(Thickness), typeof(WindowDesign));

        public static Thickness GetCaptionBorderThickness(Window element)
        {
            return (Thickness)element.GetValue(CaptionBorderThicknessProperty);
        }

        public static void SetCaptionBorderThickness(Window element, Thickness value)
        {
            element.SetValue(CaptionBorderThicknessProperty, value);
        }


    }
}
