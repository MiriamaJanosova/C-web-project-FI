using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DBRun
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var db = new AuctionSiteDbContext())
            {
                //db.Users.Add(new User()
                //{
                //    Name = "marekch"
                //});
                var users = db.Users.AsEnumerable();
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
                //db.SaveChanges();
            }

            
            Console.WriteLine("press enter to end...");
            Console.ReadLine();
        }
    }
}
