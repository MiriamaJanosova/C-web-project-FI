using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ItemCategory
    {
        public int ID { get; set; }
        public Item Item { get; set; }
        [Required]
        public int ItemID { get; set; }
        public Category Category { get; set; }
        [Required]
        public int CategoryID { get; set; }
    }
}
