using BuyerWebApi.Models;
using BuyerWebApi.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuyerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuyerController : ControllerBase
    {
        private readonly IUnitOfWork db;
        public BuyerController(IUnitOfWork _db)
        {
            this.db = _db;
        }

        [HttpPost]
        [Route("PlaceBid")]
        public ActionResult PlaceBid(Bid bid)
        {
            if (ModelState.IsValid)
            {
                Product product = this.db.product.GetProduct(bid.ProductId);

                if (product.SellerEmailId == bid.BuyerEmailId)
                    throw new Exception("Seller can't make bid");

                if (product != null && product.BidEndDate > DateTime.Now)
                {
                    this.db.bid.PlaceBid(bid);
                    return StatusCode(StatusCodes.Status201Created, bid);
                }
                else
                {
                    throw new Exception("Bid end date was: " + product.BidEndDate);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut]
        [Route("UpdateBid")]
        public ActionResult UpdateBid(Bid bid)
        {
            if (ModelState.IsValid)
            {
                this.db.bid.UpdateBid(bid);
                return StatusCode(StatusCodes.Status201Created, bid);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
