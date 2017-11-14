using Dapper;
using eShop.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace eShop.Web.Core.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SqlConnection _connection;

        public BaseRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public virtual void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> Get(Guid Id)
        {
            string sql = $"SELECT * FROM dbo.{typeof(TEntity).Name}s WHERE Id = @Id;";

            return await _connection.QueryFirstAsync<TEntity>(sql, new
            {
                Id = Id
            });
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            string sql = $"SELECT * FROM dbo.{typeof(TEntity).Name}s;";

            return await _connection.QueryAsync<TEntity>(sql);
        }
    }
}
