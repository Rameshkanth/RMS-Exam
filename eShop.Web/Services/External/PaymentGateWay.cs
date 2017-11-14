using eShop.Web.Core.Entities;

namespace eShop.Web.ViewModels
{
    public class PaymentGateWayA: CustomerPaymentService
    {
        public override bool ProcessPayment()
        {
            // Payment GateWay A logic goes here
            // send CustomerBankCard object and other values to Payment GateWay
            return true;
        }
    }
}
