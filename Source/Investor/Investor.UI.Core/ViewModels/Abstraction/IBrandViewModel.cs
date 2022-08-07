using Investor.Core;

namespace Investor.UI.Core.ViewModels
{
    public interface IBrandViewModel : IViewModel<BrandModel>
    {

        #region Public Properites

        bool IsModified { get; }

        #endregion

        #region Observable Properties

        string Name { get; set; }
        string ScaleUnit { get; set; }

        string? Description { get; set; }

        decimal? BuyPrice { get; set; }
        decimal? SellPrice { get; set; }

        #endregion

    }
}
