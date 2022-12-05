using Investor.UI.Core.ViewModels;
using InvestorData;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="IAccountViewModel"/> design instance provider.
    /// </summary>
    public class AccountDesignModel : DesingModelBase<Account>, IAccountViewModel
    {

        #region Implementation

        public string Name { get; set; }
        public string? Description { get; set; }
        public AccountType AccountType { get; set; }
        public decimal? Balance { get; set; }

        #endregion

        #region Constructor

        public AccountDesignModel()
        {
            Name = "Sales";
            Description = "Payments from your customers for products and services that your business sold.";
            AccountType = AccountType.IncomeAccount;
            Balance = 41700;
        }

        #endregion

    }
}
