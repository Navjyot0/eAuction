using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.Repository
{
    public interface IRepository<TEntity> where TEntity:class 
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        TEntity Remove(string Id);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> GetAll();
        TEntity GetById(string Id);

        TEntity Edit(TEntity entity);
    }
}
