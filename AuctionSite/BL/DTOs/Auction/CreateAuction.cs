using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Common;

namespace BL.DTOs.Auction
{
    public class CreateAuction : DtoBase
    {
        // TODO
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [MaxLength(8000)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        // TODO custom validation for numbers
        public double ActualPrice { get; set; }

        public int AuctionerID { get; set; }

        public UserDto Auctioner { get; set; }

        public List<ItemDto> AuctionedItems { get; set; } = new List<ItemDto>();
        
    }
}
