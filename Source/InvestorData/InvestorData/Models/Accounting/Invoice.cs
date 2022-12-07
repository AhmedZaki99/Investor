using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(InvoiceType), nameof(TraderId))]
    public class Invoice : BusinessEntity
    {

        public string? TraderId { get; set; }
        public Trader? Trader { get; set; }


        public InvoiceType InvoiceType { get; set; }


        public int? Number { get; set; }


        public decimal TotalAmount { get; set; }
        public decimal? AmountDue { get; set; }
        public bool IsDue => AmountDue != null;


        public bool IsTracked { get; set; }


        [Precision(0)]
        public DateTime IssueDate { get; set; }

        [Precision(0)]
        public DateTime? PaymentDue { get; set; }


        [MaxLength(1024)]
        public string? Notes { get; set; }


        public ICollection<InvoiceItem> Items { get; set; } = null!;

    }


    public enum InvoiceType
    {
        Sales = 0,
        Purchases = 1,
        ReturnSales = 2,
        ReturnPurchases = 3
    }
    // FEATURE: study the possibility to accept sales and returns in the same invoice.

}
