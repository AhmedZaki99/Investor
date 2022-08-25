using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Data
{
    public abstract class InvoiceBillBase : DatedEntity
    {

        public int? Number { get; set; }


        [Required]
        public bool IsDue { get; set; }

        [Required]
        public bool IsTracked { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        // FEATURE: study the possibility to accept sales and returns in the same invoice.


        [Required]
        [Precision(0)]
        public DateTime IssueDate { get; set; }

        [Precision(0)]
        public DateTime? PaymentDue { get; set; }


        [MaxLength(1023)]
        public string? Notes { get; set; }

    }
}
