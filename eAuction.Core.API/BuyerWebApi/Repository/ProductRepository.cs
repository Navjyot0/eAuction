using MongoDB.Driver;
using BuyerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //Product specific code here
        protected readonly IMongoCollection<Product> _dbContext;
        public ProductRepository(IMongoCollection<Product> _dbContext) : base(_dbContext)
        {
            this._dbContext = _dbContext;
        }

        public Product GetProduct(string productId)
        {
            return this._dbContext.Find(product => product.ProductId == productId).FirstOrDefault();
        }
    }
}
