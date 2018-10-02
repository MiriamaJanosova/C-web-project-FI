using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Raise> UserRaisesForAuction { get; set; }
        public List<Role> HasRoles { get; set; }
        public List<Item> Invenory { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
