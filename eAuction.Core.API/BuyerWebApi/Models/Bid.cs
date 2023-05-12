using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerWebApi.Models
{
    public class Bid
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("bidId")]
        public string BidId { get; set; }

        [BsonElement("productId")]
        public string ProductId { get; set; }

        [BsonElement("bidAmount")]
        public int BidAmount { get; set; }

        [BsonElement("buyerEmailId")]
        public string BuyerEmailId { get; set; }
    }
}
