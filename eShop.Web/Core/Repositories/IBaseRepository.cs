using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Web.DataAccess.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Guid Id);
        Task<IEnumerable<TEntity>> GetAll();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
