using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SellerWebApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("productId")]
        public string ProductId { get; set; }

        [BsonElement("productName")]
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string ProductName { get; set; }

        [BsonElement("shortDescription")]
        public string ShortDescription { get; set; }

        [BsonElement("detailedDescription")]
        public string DetailedDescription { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("startingPrice")]
        public int StartingPrice { get; set; }

        [BsonElement("bidEndDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime BidEndDate { get; set; }

        [BsonElement("sellerEmailId")]
        public string SellerEmailId { get; set; }
    }
}
