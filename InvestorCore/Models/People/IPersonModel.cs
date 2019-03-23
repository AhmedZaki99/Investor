using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestorCore.Models
{
    public interface IPersonModel
    {
        string Name { get; set; }

        string Email { get; set; }
        string WebSite { get; set; }

        AddressModel Address { get; set; }

        List<string> Phones { get; set; }
    }
}
