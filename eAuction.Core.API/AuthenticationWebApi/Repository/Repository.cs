//using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using AuthenticationWebApi.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //Generic code here
        //protected readonly DbContext _dbContext;
        protected readonly IMongoCollection<TEntity> _dbContext;

        public Repository(IMongoCollection<TEntity> dbContext)//(IProductDbSettings settings, IMongoClient mongoClient)
        {
            this._dbContext = dbContext;
            //var database = mongoClient.GetDatabase(settings.DatabaseName);
            //this._dbContext = database.GetCollection<TEntity>(settings.ProductCollectionName);
        }

        public void Add(TEntity entity)
        {
            this._dbContext.InsertOne(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this._dbContext.InsertMany(entities);
        }

        //public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        //{
        //    return this._dbContext.Where(predicate);
        //}

        //public TEntity Get(int Id)
        //{
        //    return this._dbContext.Find(Id);
        //}

        public IEnumerable<TEntity> GetAll()
        {
            return this._dbContext.Find(e=>true).ToList();
        }

        //public void Remove(TEntity entity)
        //{
        //    this._dbContext.Remove(entity);
        //}

        //public void RemoveRange(IEnumerable<TEntity> entities)
        //{
        //    this._dbContext.Set<IEnumerable<TEntity>>().RemoveRange(entities);
        //}
    }
}
