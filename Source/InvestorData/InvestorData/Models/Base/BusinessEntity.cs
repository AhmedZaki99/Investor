using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public abstract class BusinessEntity : DatedEntity
    {

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business? Business { get; set; }

    }

    public abstract class OptionalBusinessEntity : BusinessEntity
    {

        public new string? BusinessId { get; set; }

    }
}
