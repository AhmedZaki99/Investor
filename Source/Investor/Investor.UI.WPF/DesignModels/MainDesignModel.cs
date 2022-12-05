using CommunityToolkit.Mvvm.Input;
using Investor.UI.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="IMainViewModel"/> design instance provider.
    /// </summary>
    public class MainDesignModel : DesingModelBase, IMainViewModel
    {

        #region Implementation

        public ObservableCollection<ICategoryViewModel> Categories { get; set; }
        public ObservableCollection<IAccountViewModel> Accounts { get; set; }
        public ObservableCollection<IProductViewModel> Products { get; set; }
        public IProductViewModel? SelectedProduct { get; set; }
        public IEnumerable<string?> InputErrors { get; }
        public bool AddingNewProduct { get; set; }
        public string LocalStatus { get; set; }

        public ICommand CloseApplicationCommand => null!;
        public ICommand GetProductsCommand => null!;
        public IRelayCommand SaveProductCommand => null!;
        public IRelayCommand DeleteProductCommand => null!;
        public ICommand ToggleAddProductCommand => null!;
        public IRelayCommand AddProductCommand => null!;


        public Task FetchData() => throw new NotImplementedException();

        #endregion

        #region Constructor

        public MainDesignModel()
        {
            var defaultProduct = new ProductDesignModel();

            Categories = new(new CategoryDesignModel[]
            {
                (CategoryDesignModel)defaultProduct.Category!,
                new CategoryDesignModel { Name = "SmartPhones" }
            });

            Accounts = new(new AccountDesignModel[]
            {
                (AccountDesignModel)defaultProduct.SalesAccount!,
                (AccountDesignModel)defaultProduct.PurchasingAccount!,
                (AccountDesignModel)defaultProduct.InventoryAccount!
            });

            Products = new(new ProductDesignModel[]
            {
                new ProductDesignModel(),
                new ProductDesignModel
                {
                    Name = "Oppo Reno 6",
                    Code = "reno6",
                    Category = Categories[1],
                    SalesPrice = 260,
                    SalesDescription = "",
                    PurchasingPrice = 225,
                    Quantity = 27,
                    ReorderPoint = 12
                },
                new ProductDesignModel
                {
                    Name = "Samsung Note 5",
                    Code = "note5",
                    Category = Categories[1],
                    SalesPrice = 320,
                    SalesDescription = "A bit old",
                    PurchasingPrice = 305,
                    Quantity = 16,
                    ReorderPoint = 0
                }
            });

            SelectedProduct = Products.First();

            InputErrors = Enumerable.Range(1, 5).Select(i => $"This is the error #{i}");

            LocalStatus = "Design mode.";

            AddingNewProduct = true;
        }

        #endregion

    }
}
