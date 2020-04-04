using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

namespace Investor.UI.Windows
{

    public class EditableWindow : Window
    {


        #region Dependency Properties

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(double), typeof(EditableWindow));

        public static readonly DependencyProperty WindowFrameProperty =
            DependencyProperty.Register("WindowFrame", typeof(double), typeof(EditableWindow));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the height of the window caption.
        /// </summary>
        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the value of the window frame.
        /// </summary>
        public double WindowFrame
        {
            get { return (double)GetValue(WindowFrameProperty); }
            set { SetValue(WindowFrameProperty, value); }
        }

        #endregion


        #region Constructor

        static EditableWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableWindow), new FrameworkPropertyMetadata(typeof(EditableWindow)));
        }

        public EditableWindow()
        {
            // Important: Attached properties could be used directly in a style.

            var windowChrome = new WindowChrome()
            {
                CaptionHeight = this.CaptionHeight,
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(0),
                ResizeBorderThickness = new Thickness(0),
                NonClientFrameEdges = NonClientFrameEdges.None,
                UseAeroCaptionButtons = false
            };

            WindowChrome.SetWindowChrome(this, windowChrome);
        }

        #endregion


        #region Template Controls

        private Button _closeButton;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Getting template childern.
            _closeButton = GetTemplateChild("CloseButton") as Button;


            // Setting private methods.
            if (_closeButton != null) _closeButton.Click += CloseButtonClicked;
        }

        #endregion

        #region Template Controls Handlers

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
