using Dapper;
using eShop.Web.Core.Repositories;
using eShop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using System.Data;

namespace eShop.Web.DataAccess.Repositories
{
    public class UserBankAccountRepository : BaseRepository<UserBankAccount>, IUserBankAccountRepository
    {
        private readonly SqlConnection _connection;

        public UserBankAccountRepository(SqlConnection connection) : base(connection)
        {
            _connection = connection;
        }
        
        public async Task<bool> SaveBankAccount(Guid userId, string encryptedCardData)
        {
            var Id = Guid.NewGuid();

            const string sql = "INSERT INTO dbo.UserBankAccounts (Id, UserId, EncryptedCardData)" +
                                  "VALUES (@Id, @UserId, @EncryptedCardData);";

            int rowsUpdated = await _connection.ExecuteAsync(sql, new { Id = Id, UserId = userId, EncryptedCardData = encryptedCardData });

            return rowsUpdated.Equals(1);
        }

        public async Task<IEnumerable<UserBankAccount>> GetUserBankAccount(Guid UserId)
        {
            IEnumerable<UserBankAccount> userBankAccount = await _connection.QueryAsync<UserBankAccount>("GetUserBankAccounts", new
            {
                UserId = UserId
            }, commandType: CommandType.StoredProcedure);

            return userBankAccount;
        }

    }
}
