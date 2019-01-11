using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid start price")]
        public double StartPrice { get; set; }

        public double ActualPrice { get; set; }
        public IList<ImageDto> ImageBytes { get; set; } = new List<ImageDto>();
        
        public IList<HttpPostedFileBase> Upload { get; set; }

        public int UserId { get; set; }

        public UserDto Auctioner { get; set; }

        public List<ItemDto> AuctionedItems { get; set; } = new List<ItemDto>();
        
    }
}
