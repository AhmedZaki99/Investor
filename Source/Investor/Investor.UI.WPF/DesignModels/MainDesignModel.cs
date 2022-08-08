using CommunityToolkit.Mvvm.Input;
using Investor.UI.Core.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="IMainViewModel"/> design instance provider.
    /// </summary>
    public class MainDesignModel : DesingModelBase, IMainViewModel
    {

        #region Implementation

        public ObservableCollection<IBrandViewModel> Brands { get; set; }
        public IBrandViewModel? SelectedBrand { get; set; }
        public IEnumerable<string?> InputErrors { get; }
        public string LocalStatus { get; set; }
        public bool AddingNewBrand { get; set; }

        public ICommand CloseApplicationCommand => null!;
        public ICommand GetBrandsCommand => null!;
        public IRelayCommand SaveBrandCommand => null!;
        public IRelayCommand DeleteBrandCommand => null!;
        public ICommand ToggleAddBrandCommand => null!;
        public IRelayCommand AddBrandCommand => null!;

        #endregion

        #region Constructor

        public MainDesignModel()
        {
            Brands = new(new BrandDesignModel[] 
            {
                new BrandDesignModel(),
                new BrandDesignModel("Oppo Reno 6", "Unit", null, 200m, 350m),
                new BrandDesignModel("Steel grade S-37", "Ton", "Yield at 235 MPa", 470m, 600m)
            });
            SelectedBrand = Brands.First();

            InputErrors = Enumerable.Range(1, 5).Select(i => $"This is the error #{i}");

            LocalStatus = "Design mode.";

            AddingNewBrand = true;
        }

        #endregion

    }
}
