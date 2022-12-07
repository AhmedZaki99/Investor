using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Owned]
    public class TradingInfo
    {

        public string? AccountId { get; set; }
        public Account? Account { get; set; }

        public decimal Price { get; set; }

        [MaxLength(1024)]
        public string? Description { get; set; }

    }
}
