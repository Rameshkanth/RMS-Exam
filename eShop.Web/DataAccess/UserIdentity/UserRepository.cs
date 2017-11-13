using Dapper;
using eShop.Web.IdentityProvider;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.DataAccess.UserIdentity
{
    #region Interface
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByIdAsync(Guid userId);
    }
    #endregion //Interface

    public class UserRepository: IUserRepository
    {
        private readonly SqlConnection _connection;
        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        #region createuser
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            string sql = "INSERT INTO dbo.Users " +
                "VALUES (@id, @Email, @EmailConfirmed, @PasswordHash, @UserName, @FirstName, @LastName, @AddressLine1, @AddressLine2, @City, @Postcode, @Telephone)";

            int rows = await _connection.ExecuteAsync(sql, new { user.Id, user.Email, user.EmailConfirmed, user.PasswordHash, user.UserName, user.FirstName, user.LastName, user.AddressLine1, user.AddressLine2, user.City, user.Postcode, user.Telephone });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
        }
        #endregion

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            string sql = "DELETE FROM dbo.Users WHERE Id = @Id";
            int rows = await _connection.ExecuteAsync(sql, new { user.Id });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
        }


        public async Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            string sql = "SELECT * " +
                        "FROM dbo.Users " +
                        "WHERE Id = @Id;";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            {
                Id = userId
            });
        }


        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            string sql = "SELECT * " +
                        "FROM dbo.Users " +
                        "WHERE UserName = @UserName;";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            {
                UserName = userName
            });
        }
    }
}
