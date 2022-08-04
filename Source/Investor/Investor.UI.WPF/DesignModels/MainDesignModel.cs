using Investor.UI.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="IMainViewModel"/> design instance provider.
    /// </summary>
    public class MainDesignModel : IMainViewModel
    {

        #region Implementation

        public ObservableCollection<IBrandViewModel> Brands { get; set; }
        public string LocalStatus { get; set; }

        public ICommand CloseApplicationCommand => null!;
        public ICommand GetBrandsCommand => null!;

        #endregion

        #region Constructor

        public MainDesignModel()
        {
            Brands = new(new BrandDesignModel[] 
            {
                new BrandDesignModel(),
                new BrandDesignModel("Oppo Reno 6", "Device", null, 200m, 350m),
                new BrandDesignModel("Steel grade S-37", "Ton", "Yield at 235 MPa", 470m, 600m)
            });
            LocalStatus = "Design mode.";
        }

        #endregion

    }
}
