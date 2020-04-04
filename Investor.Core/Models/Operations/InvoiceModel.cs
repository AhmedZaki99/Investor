using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investor.Core.Models
{
    public class InvoiceModel : Operation
    {
        public CustomerModel Customer { get; set; }

    }
}
