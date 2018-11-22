using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class Claim : IdentityUserClaim<int>, IEntity
    {
        //public int Id { get; set; }

        [NotMapped] 
        public string TableName { get; } = "claims";
    }
}