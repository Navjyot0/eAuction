using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SellerWebApi.Models;
using SellerWebApi.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly DbContext _eAuctionContext;
        public IProductRepository product { get; private set; }
        public IUserRepository user { get; private set; }
        public IBidRepository bids { get; private set; }

        private IProductDbSettings _settings;

        public UnitOfWork(IProductDbSettings settings, IMongoClient mongoClient)//(DbContext eAuctionContext)
        {
            //this._eAuctionContext = eAuctionContext;
            //product = new ProductRepository(this._eAuctionContext);
            this._settings = settings;
            var database = mongoClient.GetDatabase(this._settings.DatabaseName);
            this.product = new ProductRepository(database.GetCollection<Product>(this._settings.ProductCollectionName));
            this.user = new UserRepository(database.GetCollection<User>("UserInfo"));
            this.bids = new BidRepository(database.GetCollection<Bid>("BuyerBids"));
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
