using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DAL.Entities
{
    public class Raise : IEntity
    {
        public int Id { get; set; }
        [NotMapped] public string TableName { get; } = "raises";

        [Required] public double Amount { get; set; }

        public Auction Auction { get; set; }

        [Required] public int AuctionId { get; set; }

        public User User { get; set; }

        [Required] public int UserId { get; set; }

        public DateTime DateTime { get; set; }
    }
}