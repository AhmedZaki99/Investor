using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public class BusinessType : EntityBase, IUniqueName
    {
        // TODO: Add Unique Index on name.


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }


        #region Rules

        [Required]
        public bool DisableServices { get; set; } = false;

        [Required]
        public bool DisableProducts { get; set; } = false;

        [Required]
        public bool NoInventory { get; set; } = false;

        [Required]
        public bool SalesOnly { get; set; } = false;

        #endregion

    }
}
