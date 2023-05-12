using MongoDB.Driver;
using AuthenticationWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        //Product specific code here
        protected readonly IMongoCollection<User> _dbContext;
        public UserRepository(IMongoCollection<User> _dbContext) : base(_dbContext)
        {
            this._dbContext = _dbContext;
        }

        public User GetUser(string UserEmailId)
        {
            return this._dbContext.Find(User => User.Email == UserEmailId).FirstOrDefault();
        }
    }
}
