using AutoMapper;
using eShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Web.ViewModels;
using eShop.Web.Entities;

namespace eShop.Web.ViewModels
{
    public class EntityFactory
    {
        public EntityFactory()
        {
            //Mapper.CreateMap<Product, ProductVm>().IgnoreAllNonExisting();
        }
        
        public ProductVm Create(Product product)
        {
            return Mapper.Map<ProductVm>(product);
        }
    }
}
