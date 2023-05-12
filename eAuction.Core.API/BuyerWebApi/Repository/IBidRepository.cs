using BuyerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.Repository
{
    public interface IBidRepository : IRepository<Bid>
    {
        void PlaceBid(Bid bid);
        void UpdateBid(Bid bid);
    }
}
