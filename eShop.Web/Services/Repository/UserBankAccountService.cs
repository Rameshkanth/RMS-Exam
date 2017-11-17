using eShop.Web.ViewModels;
using eShop.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Web.Entities;
using System.Threading.Tasks;

namespace eShop.Web.Services
{
    public interface IUserBankAccountService
    {
        Task<bool> SaveUserBankAccount(Guid UserId, string EncryptedCardData);
        Task<IEnumerable<UserBankAccount>> GetUserBankAccount(Guid UserId);
    }

    public class UserBankAccountService : IUserBankAccountService
    {
        private readonly IUserBankAccountRepository _userBankAccountRepository;

        public UserBankAccountService(IUserBankAccountRepository userBankAccountRepository)
        {
            _userBankAccountRepository = userBankAccountRepository;
        }

        public Task<bool> SaveUserBankAccount(Guid UserId, string EncryptedCardData)
        {
            return _userBankAccountRepository.SaveBankAccount(UserId, EncryptedCardData);
        }

        public Task<IEnumerable<UserBankAccount>> GetUserBankAccount(Guid UserId)
        {
            return _userBankAccountRepository.GetUserBankAccount(UserId);
        }
    }
}
