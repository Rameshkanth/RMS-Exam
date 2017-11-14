using eShop.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Web.DataAccess.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByProductName(string productName);
    }
}
