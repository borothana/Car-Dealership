namespace GuildCars.Datas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        ModelId = c.Int(nullable: false),
                        Type = c.String(),
                        BodyStyle = c.String(),
                        ReleaseYear = c.Int(nullable: false),
                        Transmission = c.String(),
                        Color = c.String(),
                        Interior = c.String(),
                        Mileage = c.Int(nullable: false),
                        VinNo = c.String(),
                        MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        IsFeature = c.Boolean(nullable: false),
                        Picture = c.Binary(),
                        AddUserId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        EditUserId = c.String(),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .Index(t => t.ModelId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        MakeId = c.Int(nullable: false),
                        AddUserId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        EditUserId = c.String(),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ModelId)
                .ForeignKey("dbo.Makes", t => t.MakeId, cascadeDelete: true)
                .Index(t => t.MakeId);
            
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        MakeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        AddUserId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        EditUserId = c.String(),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MakeId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        State = c.String(),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseType = c.String(),
                        AddUserId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        EditUserId = c.String(),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Specials",
                c => new
                    {
                        SpecialId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        FDate = c.DateTime(nullable: false),
                        TDate = c.DateTime(nullable: false),
                        image = c.Binary(),
                    })
                .PrimaryKey(t => t.SpecialId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "CarId", "dbo.Cars");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Cars", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "MakeId", "dbo.Makes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Sales", new[] { "CarId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Models", new[] { "MakeId" });
            DropIndex("dbo.Cars", new[] { "ModelId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Specials");
            DropTable("dbo.Sales");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Contacts");
            DropTable("dbo.Makes");
            DropTable("dbo.Models");
            DropTable("dbo.Cars");
        }
    }
}
