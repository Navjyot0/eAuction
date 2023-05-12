using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Repository
{
    public interface IUnitOfWork //: IDisposable
    {
        IUserRepository user { get; }
        //int Complete();
    }
}
