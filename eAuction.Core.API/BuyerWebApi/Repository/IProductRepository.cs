using BuyerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.Repository
{
    public interface IProductRepository:IRepository<Product>
    {
        //Product specific code here
        Product GetProduct(string productId);
    }
}
