
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DAL.Entities
{
    public class Category : IEntity
    {
        public int ID { get; set; }

        [NotMapped] 
        public string TableName { get; } = "categories";

        [Required]
        public ItemCategoryType CategoryType { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }
        
        //Nemusi byt
        //public List<ItemCategory> ItemsWithCategory { get; set; }
    }
}
