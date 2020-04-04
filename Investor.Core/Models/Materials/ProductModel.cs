using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investor.Core.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public ProductCategory Category { get; set; }

        public double Amount { get; set; }
        public double Minimum { get; set; }

        public string Description { get; set; }
        public string SaleInfo { get; set; }
        public string PurchaseInfo { get; set; }


    }


    public class ProductCategory
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public ProductCategory(string name)
        {
            Name = name;
        }
    }

    public class ScaleUnit
    {
        public int Id { get; private set; }

        public string Symbol { get; set; }
        public string Describtion { get; set; }

        public double ScaleFactor { get; set; }
        public ScaleType Type { get; set; }

    }

    public enum ScaleType { Quantity, Volume, Space, Expression }

}
