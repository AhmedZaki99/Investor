using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class Bill : InvoiceBillBase
    {

        [NotNull]
        public string? VendorId { get; set; }
        public Vendor? Vendor { get; set; }


        public List<BillItem> Items { get; set; } = new();

    }

}
