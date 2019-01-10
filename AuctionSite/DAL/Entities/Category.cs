
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
        public int Id { get; set; }

        [NotMapped] 
        public string TableName { get; } = "categories";

        [Index(IsUnique = true)]
        [MaxLength(300)]
        [Required]
        public string CategoryType { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }
        
        public virtual List<ItemCategory> ItemsWithCategory { get; set; }
    }
}
