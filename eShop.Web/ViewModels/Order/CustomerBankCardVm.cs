using AutoMapper;
using eShop.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Web.ViewModels
{
    public class CustomerBankCardVm
    {
        [Required]
        [Display(Name = "Name on Card")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string NameOnCard { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Cvv Number")]
        [DataType(DataType.CreditCard)]
        public string CvvNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public double ExpiryDate  { get; set; }

    }
}
