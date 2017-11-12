namespace GuildCars.Datas.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.Datas.DBContext.CarDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.Datas.DBContext.CarDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userMgr = new UserManager<GuildCars.Models.User>(new UserStore<GuildCars.Models.User>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            // Create sale role
            string curRoleID = "sale", curUserName = "sale", password = "12345678";
            if (!roleMgr.RoleExists(curRoleID))
            {
                roleMgr.Create(new IdentityRole { Name = curRoleID });
            }

            var user = new GuildCars.Models.User
            {
                UserName = curUserName
            };

            // if user existed
            if (!userMgr.Users.Any(u => u.UserName == curUserName))
            {
                userMgr.Create(user, password);
            }

            var tmpuser = userMgr.Users.Single(u => u.UserName == curUserName);
            if (!tmpuser.Roles.Any(r => r.RoleId == curRoleID))
            {
                userMgr.AddToRole(tmpuser.Id, curRoleID);
            }


            // Create admin role
            curRoleID = "admin"; curUserName = "admin"; password = "12345678";
            if (!roleMgr.RoleExists(curRoleID))
            {
                roleMgr.Create(new IdentityRole { Name = curRoleID });
            }

            user = new GuildCars.Models.User
            {
                UserName = curUserName
            };

            // if user existed
            if (!userMgr.Users.Any(u => u.UserName == curUserName))
            {
                userMgr.Create(user, password);
            }

            tmpuser = userMgr.Users.Single(u => u.UserName == curUserName);
            if (!tmpuser.Roles.Any(r => r.RoleId == curRoleID))
            {
                userMgr.AddToRole(tmpuser.Id, curRoleID);
            }
        }
    }
}
