using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DAL.Entities
{
    public class Currency : IEntity
    {
        public int Id { get; set; }

        [NotMapped] 
        public string TableName { get; } = "currencies";

        [Required]
        [MaxLength(5)]
        public string Code  { get; set; }
        
        [Required]
        public decimal ExchangeRate { get; set; }
        
        [Required]
        [MaxLength(5)]
        public string Symbol { get; set; }
    }
}
