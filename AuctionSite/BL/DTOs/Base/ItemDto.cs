using System.Collections.Generic;
using BL.DTOs.Common;

namespace BL.DTOs.Base
{
    public class ItemDto : DtoBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public List<ItemCategoryDto> HasCategories { get; set; }

        public UserDto Owner { get; set; }

        public int OwnerID { get; set; }

        public int AuctionID { get; set; }

        public AuctionDto InAuction { get; set; }

        public override string ToString()
        {
            return Name;
        }

        protected bool Equals(ItemDto other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == this.GetType() &&
                Equals((ItemDto) obj);
        }

        public override int GetHashCode()
        {
            if (Id >= 0)
            {
                return Id.GetHashCode();
            }
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Description.GetHashCode();
                hashCode = (hashCode * 397) ^ AuctionID.GetHashCode();
                hashCode = (hashCode * 397) ^ OwnerID.GetHashCode();
                hashCode = (hashCode * 397) ^ HasCategories.GetHashCode();
                return hashCode;
            }
        }
    }
}