using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investor
{
    class Transfer
    {
        public Order order { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }

        public Transfer() { }
        public Transfer(string input)
        {
            string[] Ar = input.Split('$');
            if (Ar.Length == 7)
            {
                switch (Ar[0])
                {
                    case "Deposit": order = Order.Deposit; break;
                    case "Withdraw": order = Order.Withdraw; break;
                    case "Expenses": order = Order.Expenses; break;
                }
                Type = Ar[1];
                Amount = double.Parse(Ar[2]);
                Note = Ar[3];

                Date = new DateTime(int.Parse(Ar[4]), int.Parse(Ar[5]), int.Parse(Ar[6]));
            }
        }

        public override string ToString()
        {
            string[] Vals = { order.ToString(), Type, Amount.ToString(), Note, Date.Year.ToString(), Date.Month.ToString(), Date.Day.ToString() };
            string rtrn = "";
            bool Add = false;
            foreach (string V in Vals)
                if (!Add) { rtrn += V; Add = true; }
                else rtrn += "$" + V;

            return (rtrn);
        }
    }
    
    class GTransfer
    {
        public GOrder order { get; set; }
        public BData Brand { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }

        public GTransfer() { Brand = new BData(); }
        public GTransfer(string input)
        {
            string[] Ar = input.Split('$');
            if (Ar.Length == 6)
            {
                switch (Ar[0])
                {
                    case "MoveIn": order = GOrder.MoveIn; break;
                    case "MoveOut": order = GOrder.MoveOut; break;
                }
                Brand = new BData();

                Brand.Name = Ar[1];
                Brand.TName = Ar[2];
                Brand.Scale = Ar[3];
                Amount = double.Parse(Ar[4]);
                Note = Ar[5];
            }
        }

        public override string ToString()
        {
            string[] Vals = { order.ToString(), Brand.Name, Brand.TName, Brand.Scale, Amount.ToString(), Note };
            string rtrn = "";
            bool Add = false;
            foreach (string V in Vals)
                if (!Add) { rtrn += V; Add = true; }
                else rtrn += "$" + V;

            return (rtrn);
        }
    }

    enum Order { Deposit, Withdraw, Expenses}
    enum GOrder { MoveIn, MoveOut }
}
