using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class BillItem : ItemBase
    {

        [NotNull]
        public string? ExpenseCategoryId { get; set; }
        public Account? ExpenseCategory { get; set; }


        [Required]
        public string BillId { get; set; } = null!;
        public Bill? Bill { get; set; }

    }

}
