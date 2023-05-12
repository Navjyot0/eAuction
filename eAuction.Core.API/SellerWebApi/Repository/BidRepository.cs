using SellerWebApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Repository
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        //Bid specific code here
        protected readonly IMongoCollection<Bid> dbContext;
        protected readonly IMongoCollection<User> dbUserContext;
        public BidRepository(IMongoCollection<Bid> _dbContext) : base(_dbContext)
        {
            this.dbContext = _dbContext;
        }

        public IEnumerable<Bid> GetByBidsProductId(string ProductId)
        {
            return this.dbContext.Find(b => b.ProductId == ProductId).ToList();
        }
    }
}
