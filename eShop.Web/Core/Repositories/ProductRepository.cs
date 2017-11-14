using Dapper;
using eShop.Web.DataAccess.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using eShop.Web.Entities;

namespace eShop.Web.Core.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly SqlConnection _connection;

        public ProductRepository(SqlConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Product>> GetProductsByProductName(string productName)
        {
            string sql = $"SELECT * FROM dbo.Products WHERE ProductName = @ProductName;";

            return await _connection.QueryAsync<Product>(sql, new
            {
                ProductName = productName
            });
        }

    }
}
