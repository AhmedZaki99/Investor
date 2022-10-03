using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    [Index(nameof(PaymentType), nameof(TraderId))]
    public class Payment : BusinessEntity
    {

        [NotNull]
        public string? TraderId { get; set; }
        public Trader? Trader { get; set; }


        public PaymentType PaymentType { get; set; }


        public int? Number { get; set; }

        public decimal Amount { get; set; }


        [Precision(0)]
        public DateTime PaymentDate { get; set; }


        [MaxLength(1024)]
        public string? Notes { get; set; }


        [Required]
        public string PaymentMethodId { get; set; } = null!;
        public PaymentMethod? PaymentMethod { get; set; }

    }


    public enum PaymentType
    {
        CustomerPayment = 0,
        CreditPayment = 1
    }

}
