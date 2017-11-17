using eShop.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Web.DataAccess.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize, int sortExpression, SortDirection sortDirection, string searchPhrase);
        Task<int> GetTotalNumberOfProducts();
    }
}
