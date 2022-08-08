using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Investor
{
    class Operation
    {
        public int Number { get; set; }
        public string Customer { get; set; }
        public Price price { get; set; }
        public double Amount { get; set; }
        public Trading Type { get; set; }
        public BData Brand { get; set; }
        public DateTime Time { get; set; }
        public double TotalPrice
        {
            get { return (Amount / price.Amount) * price.Value; }
        }

        public Operation() { Brand = new BData(); }
        public Operation(string input)
        {
            string[] Ar = input.Split('$');
            if (Ar.Length == 14)
            {
                Number = int.Parse(Ar[0]);
                Customer = Ar[1];
                price = new Price(double.Parse(Ar[2]), double.Parse(Ar[3]));
                Amount = double.Parse(Ar[4]);

                if (Ar[5] == "Purchase") Type = Trading.Purchase;
                else if (Ar[5] == "Sell") Type = Trading.Sell;
                else if (Ar[5] == "ReturnPur") Type = Trading.RtrnPurch;
                else Type = Trading.RtrnSales;

                Brand = new BData();

                Brand.Name = Ar[6];
                Brand.TName = Ar[7];
                Brand.Scale = Ar[8];

                Time = new DateTime(int.Parse(Ar[9]), int.Parse(Ar[10]), int.Parse(Ar[11]),
                    int.Parse(Ar[12]), int.Parse(Ar[13]), 0);
            }
        }

        public override string ToString()
        {
            string[] Vals = { Number.ToString(), Customer, price.Amount.ToString(),
                              price.Value.ToString(), Amount.ToString(), Type.ToString(),
                              Brand.Name,Brand.TName,Brand.Scale,Time.Year.ToString(),Time.Month.ToString(),
                              Time.Day.ToString(),Time.Hour.ToString(),Time.Minute.ToString() };
            string rtrn = "";
            bool Add = false;
            foreach (string V in Vals)
                if (!Add) { rtrn += V; Add = true; }
                else rtrn += "$" + V;

            return (rtrn);
        }
    }


    enum Trading { Purchase, Sell, RtrnPurch, RtrnSales }

}
