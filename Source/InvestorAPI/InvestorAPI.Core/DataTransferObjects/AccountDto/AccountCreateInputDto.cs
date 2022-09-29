using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    public class AccountCreateInputDto : AccountUpdateInputDto, IValidatableObject
    {

        #region Properties

        private string? _parentAccountId;
        public string? ParentAccountId 
        {
            get => _parentAccountId;
            set
            {
                IsSubAccount = value is not null;
                _parentAccountId = value;
            }
        }

        #endregion


        #region Constructors

        public AccountCreateInputDto()
        {
            IsSubAccount = ParentAccountId is not null;
        }

        #endregion

    }
}
