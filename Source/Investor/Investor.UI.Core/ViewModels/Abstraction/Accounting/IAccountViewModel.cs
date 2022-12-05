using InvestorData;

namespace Investor.UI.Core.ViewModels
{
    public interface IAccountViewModel : IViewModel<Account>
    {

        #region Observable Properties

        string Name { get; set; }
        string? Description { get; set; }

        AccountType AccountType { get; set; }
        decimal? Balance { get; set; }

        #endregion

    }
}
