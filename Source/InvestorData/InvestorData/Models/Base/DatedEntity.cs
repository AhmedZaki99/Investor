using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    public abstract class DatedEntity
    {

        [Required]
        [Precision(3)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }
        
        [Precision(3)]
        public DateTime? DateModified { get; set; }

    }
}
