using SellerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Repository
{
    public interface IBidRepository : IRepository<Bid>
    {
        IEnumerable<Bid> GetByBidsProductId(string ProductId);
    }
}
