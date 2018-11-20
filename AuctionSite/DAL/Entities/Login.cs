using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class Login : IdentityUserLogin<int>
    {
        //[NotMapped]
        //public string TableName { get; } = "bullshit";
    }
}