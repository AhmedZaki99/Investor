using InvestorData;

namespace InvestorAPI.Models
{
    public class BusinessTypeOutputDTO
    {

        #region Public Properties

        public string Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public bool DisableServices { get; set; }
        public bool DisableProducts { get; set; }
        public bool NoInventory { get; set; }
        public bool SalesOnly { get; set; }

        #endregion


        #region Constructors

        public BusinessTypeOutputDTO(BusinessType businessType)
        {
            Id = businessType.Id;
            
            Name = businessType.Name;
            Description = businessType.Description;

            DisableServices = businessType.DisableServices; 
            DisableProducts = businessType.DisableProducts;
            NoInventory = businessType.NoInventory;
            SalesOnly = businessType.SalesOnly;
        }

        #endregion


    }
}
