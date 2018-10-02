using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Review
    {
        public int ID { get; set; }
        public int Evaluation { get; set; }
        public string Description { get; set; }
    }
}
