using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class BillItem : ItemBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BillItemId { get; set; } = null!;


        [Required]
        public Account ExpenseCategory { get; set; } = null!;
        public string ExpenseCategoryId { get; set; } = null!;


        [Required]
        public Bill Bill { get; set; } = null!;
        public string BillId { get; set; } = null!; 

    }

}
