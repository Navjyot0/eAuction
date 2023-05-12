using SellerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        //Product specific code here
        User GetUser(string UserId);
    }
}
