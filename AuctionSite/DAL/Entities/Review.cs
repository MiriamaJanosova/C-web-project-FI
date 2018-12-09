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
    public class Review : IEntity
    {
        public int Id { get; set; }

        [NotMapped] 
        public string TableName { get; } = "reviews";

        [Required, Range(0, 10)]
        public int Evaluation { get; set; }
        
        public string Description { get; set; }
        
        public User ReviewedUser { get; set; }
        
        [Required]
        public int ReviewedUserID { get; set; }

        [Required]
        public int UserWhoWroteID { get; set; }
    }
}
