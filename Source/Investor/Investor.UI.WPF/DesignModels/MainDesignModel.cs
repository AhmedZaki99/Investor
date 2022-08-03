using Investor.UI.Core.ViewModels;
using System.Windows.Input;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="MainViewModel"/> design instance provider.
    /// </summary>
    public class MainDesignModel : IMainViewModel
    {

        #region Implementation

        public string BrandName { get; set; }

        public ICommand CloseApplicationCommand => null!;
        public ICommand ShowBrandCommand => null!;

        #endregion

        #region Constructor

        public MainDesignModel()
        {
            BrandName = "This is a very long text made to test view text wrapping, check the view out to see for yourself.";
        }

        #endregion

    }
}
