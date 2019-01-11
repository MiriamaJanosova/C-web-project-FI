using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Win32;

namespace DAL.Entities

{
    public class Auction : IEntity
    {
        public int Id { get; set; }

        [NotMapped] 
        public string TableName { get; } = "auctions";

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required, Range(0, double.MaxValue)]
        public double StartPrice { get; set; }
        
        [Range(0, double.MaxValue)]
        public double ActualPrice { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        public virtual List<Image>  ImageBytes { get; set; }
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public virtual List<Item> AuctionedItems { get; set; }
        
        public virtual List<Raise> RaisesForAuction { get; set; }

    }

}
