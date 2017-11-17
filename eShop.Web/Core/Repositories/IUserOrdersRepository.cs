using eShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Web.DataAccess.Repositories
{
    public interface IUserOrdersRepository : IBaseRepository<UserOrder>
    {
        Task<string> SaveUserOrder(Guid UserId, Guid ProductId, int Quantity,  double TotalPrice, OrderStatusType OrderStatus);
    }
}
