using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Repository
{
    public interface IUnitOfWork //: IDisposable
    {
        IProductRepository product { get; }
        IUserRepository user { get; }
        IBidRepository bids { get; }
        //int Complete();
    }
}
