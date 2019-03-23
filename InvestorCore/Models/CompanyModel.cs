using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestorCore.Models
{
    public class CompanyModel
    {
        public string Name { get; set; }

        public string EMail { get; set; }
        public string WebSite { get; set; }

        public AddressModel Address { get; set; }
        public string Currency { get; set; }

        public List<string> Phones { get; set; } = new List<string>();


        public CompanyModel(string name, AddressModel address, string currency, string eMail = null, string webSite = null, params string[] phones)
        {
            Name = name;
            Address = address;
            Currency = currency;

            eMail = EMail;
            WebSite = webSite;

            Phones = phones.ToList();

        }

    }
}
