using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public abstract class InvoiceBillBase : BusinessEntity
    {

        public int? Number { get; set; }


        [Required]
        public decimal TotalAmount { get; set; }

        public decimal? AmountDue { get; set; }
        public bool IsDue => AmountDue != null;


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


        [MaxLength(1024)]
        public string? Notes { get; set; }

    }
}
