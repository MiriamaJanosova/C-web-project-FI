using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Currency
    {
        public int ID { get; set; }
        public string Code  { get; set; }
        public double ExchangeRate { get; set; }
    }
}
