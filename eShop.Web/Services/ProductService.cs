using eShop.Web.ViewModels;
using eShop.Web.DataAccess.Repositories;
using eShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.Services
{
    public interface IProductService 
    {
        List<ProductVm> GetAllProducts();
        ProductVm GetProduct(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ProductVm> GetAllProducts()
        {
            return _productRepository.GetAll().Result.Select(x => new ProductVm
            {
                Id = x.Id,
                ProcuctName = x.ProcuctName,
                ShortDescription = x.ShortDescription,
                ImageUrl = x.ImageUrl,
                Price = x.Price
            }).ToList();
        }

        public ProductVm GetProduct(Guid id)
        {
            var productVm = _productRepository.Get(id).Result;

            return new ProductVm
            {
                Id = productVm.Id,
                ProcuctName = productVm.ProcuctName,
                ShortDescription = productVm.ShortDescription,
                ImageUrl = productVm.ImageUrl,
                Price = productVm.Price
            };
        }

    }
}
