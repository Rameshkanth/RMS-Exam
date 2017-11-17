using AutoMapper;
using eShop.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Web.Entities
{
    public class UserBankAccount
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string CvvNumber { get; set; }
        public DateTime ExpiryDate  { get; set; }
        public string EncryptedCardData { get; set; }
    }
}
