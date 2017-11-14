using eShop.Web.ViewModels;
using eShop.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace eShop.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public ProductController(
            IProductService  productService,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _productService = productService;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        public IActionResult List()
        {
            var productListVm = new ProductListVm
            {
                ProductList = _productService.GetAllProducts()
            };
            return View(productListVm);
        }

        [HttpGet]
        public IActionResult Information(Guid Id)
        {
            var productListVm = _productService.GetProduct(Id);
            return View(productListVm);
        }

        [HttpGet]
        public IActionResult Buy(Guid Id)
        {
            var productListVm = _productService.GetProduct(Id);
            return View(productListVm);
        }

    }
}
