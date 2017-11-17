using eShop.Web.ViewModels;
using eShop.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using eShop.Web.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using eShop.Web.IdentityProvider;
using eShop.Web.Helpers;
using System.Linq;

namespace eShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPaymentGateWayService _paymentGateWayService;
        private readonly IUserOrderService _userOrderService;
        private readonly IUserBankAccountService _userBankAccountService;
        private readonly RSACryptoService _RSACryptoService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
       
        public OrderController(
            IProductService  productService,
            IPaymentGateWayService paymentGateWayService,
            IUserOrderService userOrderService,
            IUserBankAccountService userBankAccountService, 
            RSACryptoService RSACryptoService,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _productService = productService;
            _paymentGateWayService = paymentGateWayService;
            _userOrderService = userOrderService;
            _userBankAccountService = userBankAccountService;
            _RSACryptoService = RSACryptoService;
            _userManager = userManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        public IActionResult Checkout(Guid Id)
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            CustomerBankCardVm scustomerBankCard = new CustomerBankCardVm();

            // Get saved bank details 
            var encryptedCustomerBankCard = _userBankAccountService.GetUserBankAccount(userId).Result;

            // If any
            if (encryptedCustomerBankCard.Any())
            {
                // Decrypt the deatils using private key stored in MachineKeyStore   - Only first one bank deatils for now
                string decryptedCustomerBankCard = _RSACryptoService.Decrypt(encryptedCustomerBankCard.First().EncryptedCardData, userId.ToString());

                // build the VM
                var customerBankCard = decryptedCustomerBankCard.Split(';');
                scustomerBankCard = new CustomerBankCardVm
                {
                    CardNumber = customerBankCard[1],
                    CvvNumber = customerBankCard[2],
                    NameOnCard = customerBankCard[0],
                    ExpiryDate = Convert.ToDateTime(customerBankCard[3])
                };
            }

            // buld CheckoutVm
            var checkoutVm = new CheckoutVm
            {
                CustomerBankCard = scustomerBankCard,
                Product = _productService.GetProduct(Id),
                SaveCardInfo = false
            };
            return View(checkoutVm);
        }

        [HttpPost]
        public IActionResult Buy(CheckoutVm checkoutVm)
        {
            if (ModelState.IsValid)
            {
                var orderConfirmationVm = new OrderConfirmationVm();

                _paymentGateWayService.CustomerBankCard = new UserBankAccount
                {
                    CardNumber = checkoutVm.CustomerBankCard.CardNumber,
                    CvvNumber = checkoutVm.CustomerBankCard.CvvNumber,
                    ExpiryDate = checkoutVm.CustomerBankCard.ExpiryDate,
                    NameOnCard = checkoutVm.CustomerBankCard.NameOnCard

                };
                _paymentGateWayService.BillingAmount = checkoutVm.Product.Price;

                var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;

                if (_paymentGateWayService.ProcessPayment())
                {
                    if (checkoutVm.SaveCardInfo) //  if customer selects to save his bank info
                    {
                        var jsondata = JsonConvert.SerializeObject(checkoutVm.CustomerBankCard);

                        // Convert the bank details to string  
                        var customerBankCard = $"{checkoutVm.CustomerBankCard.NameOnCard};{checkoutVm.CustomerBankCard.CardNumber};{checkoutVm.CustomerBankCard.CvvNumber};{checkoutVm.CustomerBankCard.ExpiryDate.ToString("dd-MM-yy")}";

                        // encrypt the bank details and store both private and public keys in MachineKeyStore with Md5Hash of userId as key identifier to retrive in the future.      
                        string encryptedCustomerBankCard = _RSACryptoService.Encrypt(customerBankCard, userId.ToString());

                        // Store the encrypted bank details in database 
                        _userBankAccountService.SaveUserBankAccount(userId, encryptedCustomerBankCard);

                        

                    }

                    // Save order in DB and return order number
                    var orderNumber = _userOrderService.SaveUserOrder(userId, checkoutVm.Product.Id, 1, checkoutVm.Product.Price, OrderStatusType.OrderPlaced).Result;

                    // Check if order has a valid order number
                    if (orderNumber != string.Empty)
                    {
                        orderConfirmationVm.Message = $"Thank you for shopping with eShop, your order has been placed and ready to shipped. Your order number is:{orderNumber}";
                    }
                    else
                    {
                        orderConfirmationVm.Message = "Sorry, there is a problem in processing your order.";
                    }

                }
                else
                {
                    //Issue with payment
                    orderConfirmationVm.Message = "Sorry, there is a problem in processing your payment.";
                }

                return View("OrderConfirmation", orderConfirmationVm);
            }

            return View("Error");
        }

    }
}
