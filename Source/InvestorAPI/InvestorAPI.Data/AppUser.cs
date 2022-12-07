using InvestorData;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Data
{
    public class AppUser : IdentityUser
    {

        [MaxLength(64)]
        public string? FirstName { get; set; }

        [MaxLength(64)]
        public string? LastName { get; set; }



        public ICollection<Business> Businesses { get; set; } = null!;


    }
}
