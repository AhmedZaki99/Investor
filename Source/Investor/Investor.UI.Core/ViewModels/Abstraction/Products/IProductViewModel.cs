using InvestorData;

namespace Investor.UI.Core.ViewModels
{
    public interface IProductViewModel : IViewModel<Product>
    {

        #region Public Properites

        bool IsModified { get; }

        #endregion

        #region Observable Properties

        bool IsService { get; set; }
        string Name { get; set; }
        string? Code { get; set; }
        ICategoryViewModel? Category { get; set; }

        IAccountViewModel? SalesAccount { get; set; }
        decimal? SalesPrice { get; set; }
        string? SalesDescription { get; set; }

        IAccountViewModel? PurchasingAccount { get; set; }
        decimal? PurchasingPrice { get; set; }
        string? PurchasingDescription { get; set; }

        IAccountViewModel? InventoryAccount { get; set; }
        double? Quantity { get; set; }
        double? ReorderPoint { get; set; }
        string? SKU { get; set; }

        #endregion

    }
}
