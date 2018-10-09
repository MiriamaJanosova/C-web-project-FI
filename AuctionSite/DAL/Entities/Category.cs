
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Category : IEntity
    {
        public int ID { get; set; }
        [Required]
        public ItemCategoryType CategoryType { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public List<ItemCategory> ItemsWithCategory { get; set; }
    }
}
