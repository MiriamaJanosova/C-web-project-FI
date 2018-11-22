using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class Login : IdentityUserLogin<int>, IEntity
    {
        [NotMapped]
        public string TableName { get; } = "bullshit";
        public int Id { get; set; }
    }
}