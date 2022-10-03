namespace InvestorData
{
    public class Customer : CustomerVendorBase
    {

        public List<Invoice> Invoices { get; set; } = new();
        public List<CustomerPayment> Payments { get; set; } = new();

    }
}
