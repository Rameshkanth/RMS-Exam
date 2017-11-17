using Dapper;
using eShop.Web.Core.Repositories;
using eShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;

namespace eShop.Web.DataAccess.Repositories
{
    public class UserOrdersRepository : BaseRepository<UserOrder>, IUserOrdersRepository
    {
        private readonly SqlConnection _connection;

        public UserOrdersRepository(SqlConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<string> SaveUserOrder(Guid UserId, Guid ProductId, int Quantity, double TotalPrice, OrderStatusType OrderStatus)
        {
            var Id = Guid.NewGuid();
            const string sql = "INSERT INTO dbo.UserOrders (Id, UserId, ProductId, Quantity, TotalPrice, OrderStatus)" +
                                  "VALUES (@Id, @UserId, @ProductId, @Quantity, @TotalPrice, @OrderStatus);";

            int rowsUpdated = await _connection.ExecuteAsync(sql, new {Id, UserId, ProductId, Quantity, TotalPrice, OrderStatus });

            return rowsUpdated.Equals(1) ? Id.ToString() : string.Empty;
        }

    }
}
