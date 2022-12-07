using InvestorData;

namespace Investor.UI.Core.ViewModels
{
    public interface ICategoryViewModel : IViewModel<Category>
    {

        #region Observable Properties

        string Name { get; set; }

        string? Description { get; set; }

        #endregion

    }
}
