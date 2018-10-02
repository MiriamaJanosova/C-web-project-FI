
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public ItemCategoryType CategoryType { get; set; }
        public string Description { get; set; }
    }
}
