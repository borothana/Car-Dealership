namespace GuildCars.Datas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "AddUserId", c => c.String());
            AddColumn("dbo.Cars", "EditUserId", c => c.String());
            AddColumn("dbo.Makes", "AddUserId", c => c.String());
            AddColumn("dbo.Makes", "EditUserId", c => c.String());
            AddColumn("dbo.Models", "AddUserId", c => c.String());
            AddColumn("dbo.Models", "EditUserId", c => c.String());
            AddColumn("dbo.Sales", "AddUserId", c => c.String());
            AddColumn("dbo.Sales", "EditUserId", c => c.String());
            DropColumn("dbo.Cars", "AddUser");
            DropColumn("dbo.Cars", "EditUser");
            DropColumn("dbo.Makes", "AddUser");
            DropColumn("dbo.Makes", "EditUser");
            DropColumn("dbo.Models", "AddUser");
            DropColumn("dbo.Models", "EditUser");
            DropColumn("dbo.Sales", "UserId");
            DropColumn("dbo.Sales", "AddUser");
            DropColumn("dbo.Sales", "EditUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "EditUser", c => c.String());
            AddColumn("dbo.Sales", "AddUser", c => c.String());
            AddColumn("dbo.Sales", "UserId", c => c.String());
            AddColumn("dbo.Models", "EditUser", c => c.String());
            AddColumn("dbo.Models", "AddUser", c => c.String());
            AddColumn("dbo.Makes", "EditUser", c => c.String());
            AddColumn("dbo.Makes", "AddUser", c => c.String());
            AddColumn("dbo.Cars", "EditUser", c => c.String());
            AddColumn("dbo.Cars", "AddUser", c => c.String());
            DropColumn("dbo.Sales", "EditUserId");
            DropColumn("dbo.Sales", "AddUserId");
            DropColumn("dbo.Models", "EditUserId");
            DropColumn("dbo.Models", "AddUserId");
            DropColumn("dbo.Makes", "EditUserId");
            DropColumn("dbo.Makes", "AddUserId");
            DropColumn("dbo.Cars", "EditUserId");
            DropColumn("dbo.Cars", "AddUserId");
        }
    }
}
