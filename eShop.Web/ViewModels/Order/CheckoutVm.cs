using eShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.ViewModels
{
    public class CheckoutVm
    {
        public CustomerBankCardVm CustomerBankCard { get; set; }
        public ProductVm Product { get; set; }

        [Display(Name = "Save Bank Details")]
        public bool SaveCardInfo { get; set; }
    }
}
