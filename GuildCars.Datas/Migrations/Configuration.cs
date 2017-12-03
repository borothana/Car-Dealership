namespace GuildCars.Datas.Migrations
{
    using GuildCars.Models;
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

            var user = new User
            {
                UserName = curUserName,
                IsActive = true
            };

            // Create Sale User
            if (!userMgr.Users.Any(u => u.UserName == curUserName))
            {
                userMgr.Create(user, password);
            }

            var userSale = userMgr.Users.Single(u => u.UserName == curUserName);
            if (!userSale.Roles.Any(r => r.RoleId == curRoleID))
            {
                userMgr.AddToRole(userSale.Id, curRoleID);
            }


            // Create admin role
            curRoleID = "admin"; curUserName = "admin"; password = "12345678";
            if (!roleMgr.RoleExists(curRoleID))
            {
                roleMgr.Create(new IdentityRole { Name = curRoleID });
            }

            user = new User
            {
                UserName = curUserName,
                IsActive = true
            };

            //  Create admin user
            if (!userMgr.Users.Any(u => u.UserName == curUserName))
            {
                userMgr.Create(user, password);
            }

            var userAdmin = userMgr.Users.Single(u => u.UserName == curUserName);
            if (!userAdmin.Roles.Any(r => r.RoleId == curRoleID))
            {
                userMgr.AddToRole(userAdmin.Id, curRoleID);
            }

            //Create make
            if (!context.Makes.Any(m => m.Description == "Toyota"))
            {
                var tmpToyota = new Make { Description = "Toyota", AddUserId = userAdmin.Id, AddDate = DateTime.Parse("11/01/2017 08:50 AM"), EditUserId = null, EditDate = DateTime.Parse("01/01/1900") };
                context.Makes.Add(tmpToyota);
                context.SaveChanges();
            }
            if (!context.Makes.Any(m => m.Description == "Saburu"))
            {
                var tmpSaburu = new Make { Description = "Saburu", AddUserId = userAdmin.Id, AddDate = DateTime.Parse("11/01/2017 08:50 AM"), EditUserId = null, EditDate = DateTime.Parse("01/01/1900") };
                context.Makes.Add(tmpSaburu);
                context.SaveChanges();
            }
            if (!context.Makes.Any(m => m.Description == "Lexus"))
            {
                var tmpLexus = new Make { Description = "Lexus", AddUserId = userAdmin.Id, AddDate = DateTime.Parse("11/03/2017 10:30 AM"), EditUserId = null, EditDate = DateTime.Parse("01/01/1900") };
                context.Makes.Add(tmpLexus);
                context.SaveChanges();
            }

            var toyota = context.Makes.First(m => m.Description == "Toyota");
            var lexus = context.Makes.First(m => m.Description == "Lexus");
            var saburu = context.Makes.First(m => m.Description == "Saburu");

            //Create model
            if (!context.Models.Any(m => m.Description == "Camry XSE"))
            {
                var camryXSE = new Model { Description = "Camry XSE", MakeId = toyota.MakeId, Make = toyota, AddUserId = userAdmin.Id, AddDate = DateTime.Parse("11/03/2017 10:30 AM"), EditUserId = null, EditDate = DateTime.Parse("01/01/1900") };
                context.Models.Add(camryXSE);
                context.SaveChanges();
            }

            if (!context.Models.Any(m => m.Description == "Camry XLE"))
            {
                var camryXLE = new Model { Description = "Camry XLE", MakeId = toyota.MakeId, Make = toyota, AddUserId = userAdmin.Id, AddDate = DateTime.Parse("11/04/2017 11:00 AM"), EditUserId = null, EditDate = DateTime.Parse("01/01/1900") };
                context.Models.Add(camryXLE);
                context.SaveChanges();
            }

            if (!context.Models.Any(m => m.Description == "RX"))
            {
                var rx300 = new Model { Description = "RX", MakeId = lexus.MakeId, Make = lexus, AddUserId = userAdmin.Id, AddDate = DateTime.Parse("11/05/2017 11:30 AM"), EditUserId = null, EditDate = DateTime.Parse("01/01/1900") };
                context.Models.Add(rx300);
                context.SaveChanges();
            }

            var xse = context.Models.First(m => m.Description == "Camry XSE");
            var xle = context.Models.First(m => m.Description == "Camry XLE");
            var lexusRX = context.Models.First(m => m.Description == "RX");

            //Create car
            //    static List<Car> _carList = new List<Car>
            //{
            //    new Car{ CarId = 1, BodyStyle = "C", Color = "red", Description = "Camry XSE 2015", Interior = "red", Mileage = 50, Type = "N",
            //        Transmission = "A", ModelId = 1, Picture = null, MSRP = 20000, SalePrice = 25000, ReleaseYear = 2016, VinNo = "1HGBH41JXMN109181",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 2, BodyStyle = "C", Color = "red", Description = "Camry XLE 2016", Interior = "gray", Mileage = 150, Type = "N",
            //        Transmission = "A", ModelId = 1, Picture = null, MSRP = 30000, SalePrice = 35000, ReleaseYear = 2016, VinNo = "1HGBH41JXMN109182",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 3, BodyStyle = "T", Color = "red", Description = "Ford Truck", Interior = "white", Mileage = 250, Type = "U",
            //        Transmission = "A", ModelId = 2, Picture = null, MSRP = 40000, SalePrice = 55000, ReleaseYear = 2017, VinNo = "1HGBH41JXMN109183",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 4, BodyStyle = "T", Color = "red", Description = "Toyota Truck", Interior = "white", Mileage = 300, Type = "U",
            //        Transmission = "A", ModelId = 2, Picture = null, MSRP = 40000, SalePrice = 65000, ReleaseYear = 2017, VinNo = "1HGBH41JXMN109184",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 5, BodyStyle = "V", Color = "red", Description = "Toyota Siana", Interior = "red", Mileage = 350, Type = "N",
            //        Transmission = "M", ModelId = 1, Picture = null, MSRP = 40000, SalePrice = 75000, ReleaseYear = 2018, VinNo = "1HGBH41JXMN109185",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 6, BodyStyle = "S", Color = "red", Description = "Lexus RX300", Interior = "red", Mileage = 50, Type = "N",
            //        Transmission = "M", ModelId = 1, Picture = null, MSRP = 40000, SalePrice = 85000, ReleaseYear = 2018, VinNo = "1HGBH41JXMN109186",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 7, BodyStyle = "V", Color = "red", Description = "Camry SE 2017", Interior = "gray", Mileage = 150, Type = "U",
            //        Transmission = "M", ModelId = 3, Picture = null, MSRP = 40000, SalePrice = 45000, ReleaseYear = 2015, VinNo = "1HGBH41JXMN109187",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 8, BodyStyle = "S", Color = "red", Description = "Camry SE 2017", Interior = "gray", Mileage = 250, Type = "U",
            //        Transmission = "M", ModelId = 3, Picture = null, MSRP = 40000, SalePrice = 55000, ReleaseYear = 2015, VinNo = "1HGBH41JXMN109188",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //    new Car{ CarId = 9, BodyStyle = "S", Color = "red", Description = "Camry SE 2016", Interior = "gray", Mileage = 350, Type = "U",
            //        Transmission = "M", ModelId = 3, Picture = null, MSRP = 40000, SalePrice = 55000, ReleaseYear = 2015, VinNo = "1HGBH41JXMN109189",
            //        AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            //};
            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109181"))
            {
                var car1 = new Car
                {
                    BodyStyle = "C",
                    Color = "red",
                    Description = "Camry XSE 2015",
                    Interior = "red",
                    Mileage = 50,
                    Type = "N",
                    Transmission = "A",
                    ModelId = xse.ModelId,
                    model = xse,
                    Picture = null,
                    MSRP = 20000,
                    SalePrice = 25000,
                    ReleaseYear = 2015,
                    VinNo = "1HGBH41JXMN109181",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109182"))
            {
                var car1 = new Car
                {
                    BodyStyle = "C",
                    Color = "gray",
                    Description = "Camry XSE 2016",
                    Interior = "gray",
                    Mileage = 100,
                    Type = "N",
                    Transmission = "A",
                    ModelId = xse.ModelId,
                    model = xse,
                    Picture = null,
                    MSRP = 35000,
                    SalePrice = 35500,
                    ReleaseYear = 2016,
                    VinNo = "1HGBH41JXMN109182",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/01/2017 10:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109183"))
            {
                var car1 = new Car
                {
                    BodyStyle = "C",
                    Color = "white",
                    Description = "Camry XSE 2017",
                    Interior = "red",
                    Mileage = 30,
                    Type = "N",
                    Transmission = "A",
                    ModelId = xse.ModelId,
                    model = xse,
                    Picture = null,
                    MSRP = 40000,
                    SalePrice = 38000,
                    ReleaseYear = 2015,
                    VinNo = "1HGBH41JXMN109183",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109184"))
            {
                var car1 = new Car
                {
                    BodyStyle = "C",
                    Color = "blue",
                    Description = "Camry XLE 2015",
                    Interior = "red",
                    Mileage = 50,
                    Type = "U",
                    Transmission = "A",
                    ModelId = xle.ModelId,
                    model = xle,
                    Picture = null,
                    MSRP = 20000,
                    SalePrice = 19000,
                    ReleaseYear = 2015,
                    VinNo = "1HGBH41JXMN109184",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109185"))
            {
                var car1 = new Car
                {
                    BodyStyle = "C",
                    Color = "red",
                    Description = "Camry XLE 2016",
                    Interior = "red",
                    Mileage = 3550,
                    Type = "U",
                    Transmission = "A",
                    ModelId = xle.ModelId,
                    model = xle,
                    Picture = null,
                    MSRP = 22000,
                    SalePrice = 20000,
                    ReleaseYear = 2016,
                    VinNo = "1HGBH41JXMN109185",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109186"))
            {
                var car1 = new Car
                {
                    BodyStyle = "C",
                    Color = "black",
                    Description = "Camry XSE 2018",
                    Interior = "black",
                    Mileage = 100,
                    Type = "N",
                    Transmission = "A",
                    ModelId = xle.ModelId,
                    model = xle,
                    Picture = null,
                    MSRP = 45000,
                    SalePrice = 42500,
                    ReleaseYear = 2018,
                    VinNo = "1HGBH41JXMN109186",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109187"))
            {
                var car1 = new Car
                {
                    BodyStyle = "V",
                    Color = "white",
                    Description = "Lexus RX 450",
                    Interior = "white",
                    Mileage = 60,
                    Type = "N",
                    Transmission = "A",
                    ModelId = lexusRX.ModelId,
                    model = lexusRX,
                    Picture = null,
                    MSRP = 60000,
                    SalePrice = 58000,
                    ReleaseYear = 2017,
                    VinNo = "1HGBH41JXMN109187",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109188"))
            {
                var car1 = new Car
                {
                    BodyStyle = "V",
                    Color = "red",
                    Description = "Lexus RX 300",
                    Interior = "red",
                    Mileage = 5000,
                    Type = "U",
                    Transmission = "A",
                    ModelId = lexusRX.ModelId,
                    model = lexusRX,
                    Picture = null,
                    MSRP = 20000,
                    SalePrice = 25000,
                    ReleaseYear = 2015,
                    VinNo = "1HGBH41JXMN109188",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            if (!context.Cars.Any(c => c.VinNo == "1HGBH41JXMN109189"))
            {
                var car1 = new Car
                {
                    BodyStyle = "V",
                    Color = "black",
                    Description = "Lexus RX 350",
                    Interior = "red",
                    Mileage = 50,
                    Type = "N",
                    Transmission = "A",
                    ModelId = lexusRX.ModelId,
                    model = lexusRX,
                    Picture = null,
                    MSRP = 55000,
                    SalePrice = 52000,
                    ReleaseYear = 2015,
                    VinNo = "1HGBH41JXMN109189",
                    AddUserId = userAdmin.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900"),
                    IsFeature = true
                };
                context.Cars.Add(car1);
                context.SaveChanges();
            }

            //Create Sale                        
            Car car = context.Cars.Take(1).ToList()[0];
            if (!context.Sales.Any(s => s.CarId == car.CarId))
            {
                var sale = new Sale
                {
                    CarId = car.CarId,
                    car = car,
                    CustomerName = "Borothana",
                    Email = "borothana@gmail.com",
                    Phone = "952 846 9047",
                    ZipCode = "55379",
                    State = "MN",
                    Street1 = "1008 Thrist Lane",
                    PurchasePrice = 37000,
                    PurchaseType = "D",
                    AddUserId = userSale.Id,
                    AddDate = DateTime.Parse("11/05/2017 11:30 AM"),
                    EditUserId = null,
                    EditDate = DateTime.Parse("01/01/1900")
                };
                context.Sales.Add(sale);
                context.SaveChanges();
            }

            //Create special
            if (!context.Specials.Any(s => s.Title == "Mid-Sale"))
            {
                var special = new Special { Title = "Mid-Sale", Description = "Discount for mid-sale....", FDate = DateTime.Parse("11/15/2017"), TDate = DateTime.Parse("12/31/2017"), image = null };
                context.Specials.Add(special);
                context.SaveChanges();
            }
            if (!context.Specials.Any(s => s.Title == "New Year"))
            {
                var special = new Special { Title = "New Year", Description = "Discount for New Year....", FDate = DateTime.Parse("11/01/2017"), TDate = DateTime.Parse("12/31/2017"), image = null };
                context.Specials.Add(special);
                context.SaveChanges();
            }
            if (!context.Specials.Any(s => s.Title == "Black Friday"))
            {
                var special = new Special { Title = "Black Friday", Description = "This Black Friday....", FDate = DateTime.Parse("11/10/2017"), TDate = DateTime.Parse("12/25/2017"), image = null };
                context.Specials.Add(special);
                context.SaveChanges();
            }
            if (!context.Specials.Any(s => s.Title == "Mid-Winter Sale"))
            {
                var special = new Special { Title = "Mid-Winter Sale", Description = "January is coming, ....", FDate = DateTime.Parse("11/01/2017"), TDate = DateTime.Parse("12/31/2017"), image = null };
                context.Specials.Add(special);
                context.SaveChanges();
            }
            if (!context.Specials.Any(s => s.Title == "10th Years Anniverary"))
            {
                var special = new Special { Title = "10th Years Anniverary", Description = "10th Years Anniverary....", FDate = DateTime.Parse("11/10/2017"), TDate = DateTime.Parse("12/22/2017"), image = null };
                context.Specials.Add(special);
                context.SaveChanges();
            }

        }
    }
}
