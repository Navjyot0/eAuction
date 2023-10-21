using MongoDB.Driver;
using AuthenticationWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Repository
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        //Product specific code here
        protected readonly IMongoCollection<Recipe> _dbContext;
        public RecipeRepository(IMongoCollection<Recipe> _dbContext) : base(_dbContext)
        {
            this._dbContext = _dbContext;
        }
    }
}
