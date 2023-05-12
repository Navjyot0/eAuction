using BuyerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBidRepository bid { get; }
        IProductRepository product { get; }
    }
}
