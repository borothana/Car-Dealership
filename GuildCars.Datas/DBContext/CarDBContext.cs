using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GuildCars.Models;

namespace GuildCars.Datas.DBContext
{
    public class CarDBContext:IdentityDbContext<User>
    {
        public CarDBContext():base("CarDBString")
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }      
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<State> States { get; set; }
    }
}
