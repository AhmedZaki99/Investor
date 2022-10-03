using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class CreditPayment : PaymentBase
    {

        [NotNull]
        public string? VendorId { get; set; } 
        public Vendor? Vendor { get; set; }

    }
}
