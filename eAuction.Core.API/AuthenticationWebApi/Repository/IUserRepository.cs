using AuthenticationWebApi.Models;
using AuthenticationWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        //Product specific code here
        User GetUser(string UserId);
    }
}
