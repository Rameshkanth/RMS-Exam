using AutoMapper;
using eShop.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Web.ViewModels
{
    public class ProductVm
    {
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Procuct Name")]
        public string ProcuctName { get; set; }

        [Display(Name = "Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
