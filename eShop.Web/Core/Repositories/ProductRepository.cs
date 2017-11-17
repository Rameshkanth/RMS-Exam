using Dapper;
using eShop.Web.DataAccess.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using eShop.Web.Entities;
using System.Data;

namespace eShop.Web.Core.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly SqlConnection _connection;

        public ProductRepository(SqlConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<int> GetTotalNumberOfProducts()
        {
            int TotalNumberOfProducts = await _connection.QuerySingleOrDefaultAsync<int>("GetTotalNumberOfProducts");

            return TotalNumberOfProducts;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize, int sortExpression, SortDirection sortDirection, string searchPhrase)
        {
            IEnumerable<Product> products = await _connection.QueryAsync<Product>("GetProducts", new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortExpression = sortExpression,
                SortDirection = sortDirection == SortDirection.Ascending ? "asc" : "desc",
                SearchPhrase = searchPhrase
            }, commandType: CommandType.StoredProcedure);

            return products;
        }

    }
}
