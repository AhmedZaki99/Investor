using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class TradingInfo : EntityBase
    {

        [NotNull]
        public string? AccountId { get; set; }
        public Account? Account { get; set; }

        public decimal Price { get; set; }

        [MaxLength(1024)]
        public string? Description { get; set; }

    }
}
