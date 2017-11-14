using eShop.Web.Core.Entities;
using eShop.Web.ViewModels;
using eShop.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using eShop.Web.Entities;

namespace eShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICustomerPaymentService _customerPaymentService;
        private readonly RSACryptoService _RSACryptoService;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
       

        public OrderController(
            IProductService  productService,
            ICustomerPaymentService customerPaymentService,
            RSACryptoService RSACryptoService,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _productService = productService;
            _customerPaymentService = customerPaymentService;
            _RSACryptoService = RSACryptoService;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        public IActionResult Checkout(Guid Id)
        {
            var checkoutVm = new CheckoutVm
            {
                CustomerBankCard = new CustomerBankCardVm(),
                Product = _productService.GetProduct(Id),
                SaveCardInfo = false
            };
            return View(checkoutVm);
        }

        [HttpPost]
        public IActionResult Buy(CheckoutVm checkoutVm)
        {
            var orderConfirmationVm = new OrderConfirmationVm();

            _customerPaymentService.CustomerBankCard = new CustomerBankCard
            {
                CardNumber = checkoutVm.CustomerBankCard.CardNumber,
                CvvNumber = checkoutVm.CustomerBankCard.CvvNumber,
                ExpiryDate = checkoutVm.CustomerBankCard.ExpiryDate,
                NameOnCard = checkoutVm.CustomerBankCard.NameOnCard

            };
            _customerPaymentService.BillingAmount = checkoutVm.Product.Price;

            if (_customerPaymentService.ProcessPayment())
            {
                if (checkoutVm.SaveCardInfo) //  if customer selects to save his bank info
                {
                    // Convert the bank details to string  
                    var customerBankCard = $"{checkoutVm.CustomerBankCard.CardNumber};{checkoutVm.CustomerBankCard.CvvNumber};{checkoutVm.CustomerBankCard.ExpiryDate}";

                    // encrypt it using RSA and get the private key as output parameter and encryped string
                    RSAParameters privateKey;
                    string encryptedCustomerBankCard = _RSACryptoService.Encrypt(customerBankCard, out privateKey);

                    // TO DO: Store the RSA key (privateKey) object in safe place (Not in database with customer bank info)  - need investigation into this
                    // TO DO: Store the encrypted bank details in database 

                }

                // TO DO: Save the order and generate the order number 

                orderConfirmationVm.Message = "Thank you for shopping with eShop, your order has been completed.";
            }
            else
            {
                orderConfirmationVm.Message = "Sorry, there is a problem in processing your order.";
            }

            return View("OrderConfirmation", orderConfirmationVm);
        }

    }
}
