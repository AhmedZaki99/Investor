using Investor.UI.Core.ViewModels;
using InvestorData;
using System;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="IProductViewModel"/> design instance provider.
    /// </summary>
    public class ProductDesignModel : DesingModelBase<Product>, IProductViewModel
    {

        #region Implementation

        public bool IsModified => throw new NotImplementedException();

        public bool IsService { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public ICategoryViewModel? Category { get; set; }

        public IAccountViewModel? SalesAccount { get; set; }
        public decimal? SalesPrice { get; set; }
        public string? SalesDescription { get; set; }

        public IAccountViewModel? PurchasingAccount { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public string? PurchasingDescription { get; set; }

        public IAccountViewModel? InventoryAccount { get; set; }
        public double? Quantity { get; set; }
        public double? ReorderPoint { get; set; }
        public string? SKU { get; set; }

        #endregion

        #region Constructor

        public ProductDesignModel()
        {
            IsService = false;
            Name = "Dell Inspiron 3580 Laptop";
            Code = "dell3580";
            Category = new CategoryDesignModel();

            SalesAccount = new AccountDesignModel();
            SalesPrice = 779.99m;
            SalesDescription = "Features Intel Core i5 8th-Gen CPU, with 8 GB Memory, and a Radeon 520 HD GPU";

            PurchasingAccount = new AccountDesignModel
            {
                Name = "Purchases – Hardware for Resale",
                AccountType = AccountType.ExpenseAccount
            };
            PurchasingPrice = 520;
            PurchasingDescription = "Takes 3 days for shipping.";

            InventoryAccount = new AccountDesignModel
            {
                Name = "Hardware",
                AccountType = AccountType.AssetsAccount
            };
            Quantity = 18;
            ReorderPoint = 5;
        }

        #endregion

    }
}
