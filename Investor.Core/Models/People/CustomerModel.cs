using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investor.Core.Models
{
    public class CustomerModel : IPersonModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public string WebSite { get; set; }

        public AddressModel Address { get; set; }

        public List<string> Phones { get; set; } = new List<string>();
    }
}
