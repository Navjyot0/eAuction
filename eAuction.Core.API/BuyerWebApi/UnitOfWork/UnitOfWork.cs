using BuyerWebApi.Models;
using BuyerWebApi.Repository;
using BuyerWebApi.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBuyerDbSettings _settings;
        public IBidRepository bid { get; private set; }
        public IProductRepository product { get; private set; }

        public UnitOfWork(IBuyerDbSettings settings, IMongoClient mongoClient)//(DbContext eAuctionContext)
        {
            //this._eAuctionContext = eAuctionContext;
            //product = new ProductRepository(this._eAuctionContext);
            this._settings = settings;
            var database = mongoClient.GetDatabase(this._settings.DatabaseName);
            this.bid = new BidRepository(database.GetCollection<Bid>(this._settings.CollectionName));
            this.product = new ProductRepository(database.GetCollection<Product>("Products"));
        }
    }
}
