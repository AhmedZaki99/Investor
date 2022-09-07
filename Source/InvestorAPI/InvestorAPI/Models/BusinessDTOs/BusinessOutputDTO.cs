using InvestorData;

namespace InvestorAPI.Models
{
    public class BusinessOutputDTO
    {

        #region Public Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public string? BusinessTypeId { get; set; }
        public BusinessType? BusinessType { get; set; }

        public string? Country { get; set; }
        public string? Currency { get; set; }

        #endregion


        #region Constructors

        public BusinessOutputDTO(Business business)
        {
            Id = business.Id;
            Name = business.Name;
            
            BusinessTypeId = business.BusinessTypeId;
            BusinessType = business.BusinessType;

            Country = business.Country;
            Currency = business.Currency;
        }

        #endregion


    }
}
