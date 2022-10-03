using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    public class BusinessType : EntityBase, IUniqueName
    {

        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }


        #region Rules

        public bool DisableServices { get; set; } = false;

        public bool DisableProducts { get; set; } = false;

        public bool NoInventory { get; set; } = false;

        public bool SalesOnly { get; set; } = false;

        #endregion

    }
}
