using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Common;

namespace BL.DTOs.Item
{
    public class CreateItem : DtoBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int OwnerID { get; set; }

<<<<<<< HEAD
        public List<CategoryDto> HasCategories { get; set; }
=======
        public List<ItemCategoryDto> HasCategories { get; set; }
>>>>>>> origin/marek-branch

        // TODO more fields 
    }
}
