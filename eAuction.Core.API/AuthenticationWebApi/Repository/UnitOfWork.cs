using MongoDB.Driver;
using AuthenticationWebApi.Models;
using AuthenticationWebApi.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly DbContext _eAuctionContext;
        public IUserRepository user { get; private set; }
        public IRecipeRepository recipe { get; private set; }
        private IUserDbSettings _settings;

        //public UnitOfWork(IUserDbSettings settings, IMongoClient mongoClient)//(DbContext eAuctionContext)
        public UnitOfWork(IUserDbSettings settings)
        {
            //this._eAuctionContext = eAuctionContext;
            //product = new ProductRepository(this._eAuctionContext);

            this._settings = settings;
            MongoClient mongoClient = new MongoClient(this._settings.ConnectionString);
            var database = mongoClient.GetDatabase(this._settings.DatabaseName);
            this.user = new UserRepository(database.GetCollection<User>(this._settings.CollectionName));
            this.recipe = new RecipeRepository(database.GetCollection<Recipe>("Recipe"));
        }

        //public int Complete()
        //{
        //    return _eAuctionContext.SaveChanges();
        //}

        //public void Dispose()
        //{
        //    _eAuctionContext.Dispose();
        //}
    }
}
