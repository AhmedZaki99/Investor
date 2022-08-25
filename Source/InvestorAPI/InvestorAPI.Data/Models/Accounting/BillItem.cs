using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorAPI.Data
{
    public class BillItem : ItemBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BillItemId { get; set; } = null!;


        [NotNull]
        public Account? ExpenseCategory { get; set; }
        public string? ExpenseCategoryId { get; set; }


        [Required]
        public string BillId { get; set; } = null!; 
        public Bill Bill { get; set; } = null!;

    }

}
