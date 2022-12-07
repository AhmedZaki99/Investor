using Investor.UI.Core.ViewModels;
using InvestorData;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="ICategoryViewModel"/> design instance provider.
    /// </summary>
    public class CategoryDesignModel : DesingModelBase<Category>, ICategoryViewModel
    {

        #region Implementation

        public string Name { get; set; }
        public string? Description { get; set; }

        #endregion

        #region Constructor

        public CategoryDesignModel()
        {
            Name = "Laptops";
        }

        #endregion

    }
}
