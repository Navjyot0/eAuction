using BuyerWebApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.Repository
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        //Bid specific code here
        protected readonly IMongoCollection<Bid> dbContext;
        public BidRepository(IMongoCollection<Bid> _dbContext) : base(_dbContext)
        {
            this.dbContext = _dbContext;
        }

        public void PlaceBid(Bid bid)
        {
            Bid currentBid = this.dbContext.Find(b => b.BuyerEmailId == bid.BuyerEmailId && b.ProductId == bid.ProductId).FirstOrDefault();
            
            if (currentBid == null)
                this.dbContext.InsertOne(bid);
            else
            {
                currentBid.ProductId = bid.ProductId;
                currentBid.BidAmount = bid.BidAmount;
                currentBid.BuyerEmailId = bid.BuyerEmailId;
                this.dbContext.ReplaceOne(b => b.BuyerEmailId == bid.BuyerEmailId, currentBid);
            }
        }

        public void UpdateBid(Bid bid)
        {
            Bid currentBid = this.dbContext.Find(b => b.BuyerEmailId == bid.BuyerEmailId && b.ProductId == bid.ProductId).FirstOrDefault();

            if (currentBid == null)
                this.dbContext.InsertOne(bid);
            else
            {
                currentBid.ProductId = bid.ProductId;
                currentBid.BidAmount = bid.BidAmount;
                currentBid.BuyerEmailId = bid.BuyerEmailId;
                this.dbContext.ReplaceOne(b => b.BuyerEmailId == bid.BuyerEmailId, currentBid);
            }
        }
    }
}
