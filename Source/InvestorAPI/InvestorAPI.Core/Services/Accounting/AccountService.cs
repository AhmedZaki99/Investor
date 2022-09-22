using AutoMapper;
using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Account"/> models.
    /// </summary>
    internal class AccountService : IAccountService
    {

        #region Dependencies

        private readonly IAccountRepository _accountRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IBusinessTypeRepository _businessTypeRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AccountService(IAccountRepository accountRepository,
                              IBusinessRepository businessRepository,
                              IBusinessTypeRepository businessTypeRepository,
                              IMapper mapper)
        {
            _accountRepository = accountRepository;
            _businessRepository = businessRepository;
            _businessTypeRepository = businessTypeRepository;
            _mapper = mapper;
        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public async Task<IDictionary<string, string>> ValidateAccountAsync(AccountInputDto dto, Account? original = null)
        {
            IDictionary<string, string> errors = new Dictionary<string, string>();

            if (dto.BusinessId != original?.BusinessId && !ValidateId(_businessRepository, dto.BusinessId, out errors))
            {
                return errors;
            }
            if (dto.BusinessTypeId != original?.BusinessTypeId && !ValidateId(_businessTypeRepository, dto.BusinessTypeId, out errors))
            {
                return errors;
            }

            if (dto.BusinessId is not null && dto.BusinessTypeId is not null)
            {
                errors.Add("BusinessId / BusinessTypeId", "Accounts can't be assigned for both Business and BusinessType, try providing only one of them.");
                return errors;
            }

            if (dto.ParentAccountId != original?.ParentAccountId && dto.ParentAccountId is not null)
            {
                var parentAccount = await _accountRepository.GetMinimalDataAsync(dto.ParentAccountId);

                string? errorMsg = parentAccount is not null ?
                                   parentAccount.IsSubAccount ?
                                   "The account provided has a parent account on its own, thus it can't be assigned as a parent account." : null :
                                   "There's no Account found with the Id provided.";
                if (errorMsg is not null)
                {
                    errors.Add(nameof(dto.ParentAccountId), errorMsg);
                    return errors;
                }
            }

            return errors;
        }

        #endregion


        #region Helper Methods

        private static bool ValidateId<T>(IRepository<T> repository, string? id, out IDictionary<string, string> errors) where T : class, IStringId
        {
            errors = new Dictionary<string, string>();

            if (id is not null && !repository.EntityExists(id))
            {
                errors.Add($"{typeof(T).Name}Id", $"There's no {typeof(T).Name} found with the Id provided.");
                return false;
            }
            return true;
        }

        #endregion

    }
}
