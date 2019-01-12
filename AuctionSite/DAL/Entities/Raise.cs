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

        [Required] public decimal Amount { get; set; }

        public virtual Auction Auction { get; set; }

        [Required] public int AuctionId { get; set; }

        public virtual User User { get; set; }

        [Required] public int UserId { get; set; }

        public DateTime DateTime { get; set; }
    }
}