using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Common;

namespace BL.DTOs.Item
{
    public class CreateItem : DtoBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int OwnerID { get; set; }

        // TODO more fields 
    }
}
