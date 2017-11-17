using AutoMapper;
using eShop.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Web.ViewModels
{
    // To be moved to appropriate place
    public class CardExpiryDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }

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

        [Required]
        [Display(Name = "Cvv Number")]
        [DataType(DataType.CreditCard)]
        public string CvvNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CardExpiryDate(ErrorMessage = "Your card is expired, please enter a valid expiry date or use another card")]
        [Display(Name = "Expiry Date ")]
        public DateTime ExpiryDate  { get; set; } = DateTime.Now;

    }
}
