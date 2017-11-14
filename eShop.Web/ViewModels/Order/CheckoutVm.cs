using eShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.ViewModels
{
    public class CheckoutVm
    {
        public CustomerBankCardVm CustomerBankCard { get; set; }
        public ProductVm Product { get; set; }
        public bool SaveCardInfo { get; set; }
    }
}
