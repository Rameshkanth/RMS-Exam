using eShop.Web.Entities;

namespace eShop.Web.Core.Entities
{
    public interface ICustomerPaymentService
    {
        CustomerBankCard CustomerBankCard { get; set; }
        string CardholderName { get; set; }
        string CardholderAddress { get; set; }
        string CardholderPostcode { get; set; }
        string Email { get; set; }
        string BillingAddress { get; set; }
        string BillingFirstName { get; set; }
        string BillingLastName { get; set; }
        string BillingPostalCode { get; set; }
        double BillingAmount { get; set; }
        bool ProcessPayment();
    }

    public abstract class CustomerPaymentService : ICustomerPaymentService
    {
        public CustomerBankCard CustomerBankCard { get; set; }
        public string CardholderName { get; set; }
        public string CardholderAddress { get; set; }
        public string CardholderPostcode { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingPostalCode { get; set; }
        public double BillingAmount { get; set; }
        public abstract bool ProcessPayment();
    }
}
