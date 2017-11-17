using eShop.Web.ViewModels;
using eShop.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Web.Entities;
using System.Threading.Tasks;

namespace eShop.Web.Services
{
    public interface IProductService 
    {
        List<ProductVm> GetAllProducts();
        IEnumerable<ProductVm> GetAllProducts(int pageNumber, int pageSize, int sortExpression, SortDirection sortDirection, string searchPhrase);
        ProductVm GetProduct(Guid id);
        int GetTotalNumberOfProducts();
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

        public int GetTotalNumberOfProducts()
        {
            return _productRepository.GetTotalNumberOfProducts().Result;
        }

        public IEnumerable<ProductVm> GetAllProducts(int pageNumber, int pageSize, int sortExpression, SortDirection sortDirection, string searchPhrase)
        {
            return _productRepository.GetProductsAsync(pageNumber, pageSize, sortExpression, sortDirection, searchPhrase).Result.Select(x => new ProductVm
            {
                Id = x.Id,
                ProcuctName = x.ProcuctName,
                ShortDescription = x.ShortDescription,
                ImageUrl = x.ImageUrl,
                Price = x.Price
            });
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
