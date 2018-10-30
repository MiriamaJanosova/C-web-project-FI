using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DAL.Entities
{
    public class EmailTemplate : IEntity
    {
        public int ID  { get; set; }

        [NotMapped] 
        public string TableName { get; } = "email_templates";
        
        public string Message { get; set; }
    }
}
