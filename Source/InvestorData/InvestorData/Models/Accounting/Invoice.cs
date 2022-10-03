using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class Invoice : InvoiceBillBase
    {

        [NotNull]
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        public List<InvoiceItem> Items { get; set; } = new();

    }

}
