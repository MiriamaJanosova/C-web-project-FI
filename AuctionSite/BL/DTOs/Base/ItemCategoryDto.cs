using BL.DTOs.Common;
using BL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class ItemCategoryDto : DtoBase
    {
        public int ItemID { get; set; }

        public ItemDto Item { get; set; }

        public int CategoryID { get; set; }

        public CategoryDto Category { get; set; }


        public override string ToString()
        {
            return $"Item {Item.Name} has category {Category.CategoryType.ToString()}";
        }

        protected bool Equals(ItemCategoryDto other)
        {
            if (Id == other.Id)
            {
                return true;
            }
            return Item.Equals(other.Item) &&
                ItemID == other.ItemID &&
                CategoryID == other.CategoryID &&
                Category == other.Category;
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
                Equals((ItemCategoryDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ ItemID.GetHashCode();
                hashCode = (hashCode * 397) ^ CategoryID.GetHashCode();
                if (Item != null)
                {
                    hashCode = (hashCode * 397) ^ Item.GetHashCode();
                }
                
                if (Category != null)
                {
                    hashCode = (hashCode * 397) ^ Category.GetHashCode();
                }
                return hashCode;
            }
        }
    }
}
