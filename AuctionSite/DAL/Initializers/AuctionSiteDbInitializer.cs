using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Initializers
{
    public class AuctionSiteDbInitializer : DropCreateDatabaseAlways<AuctionSiteDbContext>
    {
        protected override void Seed(AuctionSiteDbContext context)
        {
            context.EmailTemplates.Add(new EmailTemplate
            {
                Message = "Nejaka zprava"
            });

            context.Currencies.Add(new Currency
            {
                Code = "CZK",
                ExchangeRate = 150
            });

            context.Roles.Add(new Role
            {
                Name = "Admin"
            });

            context.Roles.Add(new Role
            {
                Name = "User"
            });

            context.Users.Add(new User
            {
                Name = "Meno",
                Surname = "Prijmeni"
            });

            base.Seed(context);
        }
    }
}
