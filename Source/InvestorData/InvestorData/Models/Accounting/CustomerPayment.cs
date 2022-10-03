using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class CustomerPayment : PaymentBase
    {

        [NotNull]
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; } 

    }
}
