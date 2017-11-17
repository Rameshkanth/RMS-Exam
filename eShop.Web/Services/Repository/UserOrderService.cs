using eShop.Web.ViewModels;
using eShop.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Web.Entities;
using System.Threading.Tasks;

namespace eShop.Web.Services
{
    public interface IUserOrderService
    {
        Task<string> SaveUserOrder(Guid UserId, Guid ProductId, int Quantity, double TotalPrice, OrderStatusType OrderStatus);
    }

    public class UserOrderService : IUserOrderService
    {
        private readonly IUserOrdersRepository _userOrdersRepository;

        public UserOrderService(IUserOrdersRepository userOrdersRepository)
        {
            _userOrdersRepository = userOrdersRepository;
        }

        public Task<string> SaveUserOrder(Guid UserId, Guid ProductId, int Quantity, double TotalPrice, OrderStatusType OrderStatus)
        {
            return _userOrdersRepository.SaveUserOrder(UserId, ProductId, Quantity, TotalPrice, OrderStatus);
        }

    }
}
