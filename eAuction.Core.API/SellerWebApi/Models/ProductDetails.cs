using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Models
{
    public class ProductDetails
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public int StartingPrice { get; set; }
        public DateTime BidEndDate { get; set; }
        public User SellerDetails { get; set; }
        public IEnumerable<Bid> Bids { get; set; }
    }
}
