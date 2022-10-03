using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public class Contact : DatedEntity
    {

        [MaxLength(64)]
        public string? FirstName { get; set; }

        [MaxLength(64)]
        public string? LastName { get; set; }


        [MaxLength(128)]
        public string? Email { get; set; }

        [MaxLength(32)]
        public string? Phone { get; set; }

    }
}
