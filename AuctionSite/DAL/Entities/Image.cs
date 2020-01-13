using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Infrastructure;

namespace DAL.Entities
{
    public class Image : IEntity
    {
        public byte[] Bytes { get; set; }
        public int Id { get; set; }
        public string TableName { get; } = "images";
        
        [Required, ForeignKey("Auction")]
        public int AuctionId { get; set; }
        
        public Auction Auction { get; set; }
    }
}