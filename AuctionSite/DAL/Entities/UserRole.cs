using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class UserRole : IdentityUserRole<int>, IEntity
    {
        [NotMapped]
        public string TableName { get; } = "user_roles";
        public int Id { get; set; }
    }
}