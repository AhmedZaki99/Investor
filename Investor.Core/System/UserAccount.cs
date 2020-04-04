using System;
using Investor.Core.Models;

namespace Investor.Core
{
    public class UserAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AddressModel Address { get; set; }
        public DateTime BirthDate { get; set; }

        public bool IsAdmin { get; set; }


        public UserAccount(bool admin, string first, string email, string password, AddressModel address, string phone = null, string last = null, DateTime birth = new DateTime())
        {
            FirstName = first;
            LastName = last;

            Phone = phone;
            Email = email;
            Password = password;

            Address = address;
            BirthDate = birth;

            IsAdmin = admin;
        }
    }
}
