using BL.DTOs.Common;
using DAL.Entities;

namespace BL.DTOs.Base
{
    public class ImageDto : DtoBase
    {
        public byte[] Bytes { get; set; }
        
        public int AuctionId { get; set; }
        
        public AuctionDto Auction { get; set; }

        public ImageDto(byte[] bytes)
        {
            Bytes = bytes;
        }
    }
}