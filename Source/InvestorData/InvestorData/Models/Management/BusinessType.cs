using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    public class BusinessType : IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


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
