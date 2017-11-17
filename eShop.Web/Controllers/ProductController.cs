using eShop.Web.ViewModels;
using eShop.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using eShop.Web.Core.Entities;
using System.Linq;
using eShop.Web.Entities;

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
        [ActionName("ProductSearch")]
        public IActionResult ProductList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllProductsAsJson(DataTable dataTable)
        {
            int pageNumber = dataTable.Start / dataTable.Length + 1;
            Order order = dataTable.Order.FirstOrDefault();

            SortDirection sortDirection = order != null
                ? (order.Direction == "asc" ? SortDirection.Ascending : SortDirection.Descending)
                : SortDirection.Ascending;

            ProductVm[] products = (_productService.GetAllProducts(pageNumber, dataTable.Length, order?.Column ?? 0, sortDirection,
                string.IsNullOrEmpty(dataTable.Search.Value) ? string.Empty : dataTable.Search.Value)).ToArray();

            int totalNumberOfProducts = _productService.GetTotalNumberOfProducts();

            JsonResult jsonResult =  Json(new DataTableResponse<ProductVm>
            {
                Data = products,
                RecordsFiltered = string.IsNullOrEmpty(dataTable.Search.Value) ? totalNumberOfProducts : products.Length,
                Draw = dataTable.Draw,
                RecordsTotal = totalNumberOfProducts
            });

            return jsonResult;
        }

        [HttpGet]
        public IActionResult ProductDetails(Guid Id)
        {
            var productListVm = _productService.GetProduct(Id);
            return View(productListVm);
        }

        [HttpGet]
        public IActionResult Buy(Guid Id)
        {
            var productVm = _productService.GetProduct(Id);
            return View(productVm);
        }

    }
}
