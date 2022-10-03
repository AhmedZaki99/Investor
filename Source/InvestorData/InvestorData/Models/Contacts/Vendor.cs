namespace InvestorData
{
    public class Vendor : CustomerVendorBase
    {

        public List<Bill> Bills { get; set; } = new();
        public List<CreditPayment> Payments { get; set; } = new();

    }
}
