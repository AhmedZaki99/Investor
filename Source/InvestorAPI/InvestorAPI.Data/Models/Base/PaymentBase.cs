using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public abstract class PaymentBase : DatedEntity
    {

        public int? Number { get; set; }

        [Required]
        public decimal Amount { get; set; }


        [Required]
        [Precision(0)]
        public DateTime PaymentDate { get; set; }


        [MaxLength(1024)]
        public string? Notes { get; set; }

    }
}
