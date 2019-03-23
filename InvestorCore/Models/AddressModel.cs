using System;

namespace InvestorCore.Models
{
    public class AddressModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public int PostalCode { get; set; }


        public AddressModel(string country, string city = null, string street = null, int postalCode = 0)
        {
            Country = country;

            City = city;
            Street = street;

            PostalCode = postalCode;
        }

    }
}
