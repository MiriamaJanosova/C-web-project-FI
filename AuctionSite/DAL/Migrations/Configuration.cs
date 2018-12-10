
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.AuctionSiteDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}