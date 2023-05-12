using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebApi.Models;
using SellerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SellerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork db;
        public ProductController(IUnitOfWork _db)
        {
            this.db = _db;
        }

        // POST api/<ProductController>
        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                this.db.product.Add(product);
                return StatusCode(StatusCodes.Status201Created, product);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet]
        [Route("GetProductList")]
        public ActionResult<IEnumerable<Product>> GetProductList()
        {
            return Ok(this.db.product.GetAll());
        }

        [HttpGet]
        [Route("GetProductDetails")]
        public ActionResult<Product> GetProductDetails(string productId)
        {
            Product product = this.db.product.GetProduct(productId);
            ProductDetails productDetails = new ProductDetails()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ShortDescription = product.ShortDescription,
                DetailedDescription = product.DetailedDescription,
                Category = product.Category,
                StartingPrice = product.StartingPrice,
                BidEndDate = product.BidEndDate,
                SellerDetails = this.db.user.GetUser(product.SellerEmailId)
            };

            List<Bid> bids = this.db.bids.GetByBidsProductId(product.ProductId).ToList();
            List<Bid> allBids = new List<Bid>();
            foreach (Bid b in bids)
            {
                User _user = this.db.user.GetUser(b.BuyerEmailId);
                b.BuyerName = _user.FirstName + " " + _user.LastName;
                b.BuyerPhone = _user.Phone;
                allBids.Add(b);
            }
            productDetails.Bids = allBids;

            return StatusCode(StatusCodes.Status201Created, productDetails);
        }

        [HttpGet]
        [Route("DeleteProduct")]
        public ActionResult DeleteProduct(string productId)
        {
            Product product = this.db.product.GetProduct(productId);
            ProductDetails productDetails = new ProductDetails()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ShortDescription = product.ShortDescription,
                DetailedDescription = product.DetailedDescription,
                Category = product.Category,
                StartingPrice = product.StartingPrice,
                BidEndDate = product.BidEndDate,
                SellerDetails = this.db.user.GetUser(product.SellerEmailId),
                Bids = this.db.bids.GetByBidsProductId(product.ProductId).ToList()
            };

            if (productDetails.BidEndDate < DateTime.Now)
                throw new Exception("Can't delete: bid end date exided");
            if (productDetails.Bids.Count() > 0)
                throw new Exception("Can't delete: there are already bids");
            this.db.product.DeleteProduct(productId);
            return StatusCode(StatusCodes.Status202Accepted, productId + "Deleted Successfully");
        }
    }
}
