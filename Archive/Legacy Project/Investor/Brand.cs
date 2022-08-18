using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Investor
{
    class BType
    {
        public string Name { get; set; }
        public List<Brand> Brands { get; set; }

        public BType(string name) { Name = name; Brands = new List<Brand>(); }
        public BType(string name, List<Brand> brands) { Name = name; Brands = brands; }
    }

    class Brand
    {
        public string Name { get; set; }
        public string ScaleUnit { get; set; }
        public Price TradePrice { get; set; }
        public Price SellPrice { get; set; }
        public double Amount { get; set; }
        public double Value { get; set; }
        public BType Type { get; set; }

        public Brand() { }
        public Brand(string input)
        {
            string[] Ar = input.Split('$');
            if (Ar.Length == 9)
            {
                Name = Ar[0];
                ScaleUnit = Ar[1];
                TradePrice = new Price(double.Parse(Ar[2]), double.Parse(Ar[3]));
                SellPrice = new Price(double.Parse(Ar[4]), double.Parse(Ar[5]));
                Amount = double.Parse(Ar[6]);
                Value = double.Parse(Ar[7]);
                Type = new BType(Ar[8]);
            }
        }

        public override string ToString()
        {
            string[] Vals = { Name, ScaleUnit, TradePrice.Amount.ToString(),
                              TradePrice.Value.ToString(), SellPrice.Amount.ToString(),
                              SellPrice.Value.ToString(), Amount.ToString(), Value.ToString(), Type.Name };
            string rtrn = "";
            bool Add = false;
            foreach (string V in Vals)
                if (!Add) { rtrn += V; Add = true; }
                else rtrn += "$" + V;

            return (rtrn);
        }
    }

    class Price
    {
        public double Amount { get; set; }
        public double Value { get; set; }

        public Price() { }
        public Price(double A, double B) { Amount = A; Value = B; }
    } 

    class BData
    {
        public string Name { get; set; }
        public string TName { get; set; }
        public string Scale { get; set; }
    }
}
