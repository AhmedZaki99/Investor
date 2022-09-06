using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class BillItem : ItemBase, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


        [NotNull]
        public string? ExpenseCategoryId { get; set; }
        public Account? ExpenseCategory { get; set; }


        [Required]
        public string BillId { get; set; } = null!;
        public Bill? Bill { get; set; }

    }

}
