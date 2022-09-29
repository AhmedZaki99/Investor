using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    public class Contact : DatedEntity, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


        [MaxLength(64)]
        public string? FirstName { get; set; }

        [MaxLength(64)]
        public string? LastName { get; set; }


        [MaxLength(128)]
        public string? Email { get; set; }

        [MaxLength(32)]
        public string? Phone { get; set; }

    }
}
