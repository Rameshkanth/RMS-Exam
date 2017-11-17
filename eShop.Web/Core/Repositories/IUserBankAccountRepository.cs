using eShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Web.DataAccess.Repositories
{
    public interface IUserBankAccountRepository : IBaseRepository<UserBankAccount>
    {
        Task<bool> SaveBankAccount(Guid UserId, string EncryptedCardData);
        Task<IEnumerable<UserBankAccount>> GetUserBankAccount(Guid UserId);
    }
}
