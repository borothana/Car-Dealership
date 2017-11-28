namespace GuildCars.Datas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Transmission", c => c.String());
            AddColumn("dbo.Cars", "IsFeature", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sales", "City", c => c.String());
            AddColumn("dbo.Specials", "FDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Specials", "TDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Cars", "Transmision");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Transmision", c => c.String());
            DropColumn("dbo.Specials", "TDate");
            DropColumn("dbo.Specials", "FDate");
            DropColumn("dbo.Sales", "City");
            DropColumn("dbo.Cars", "IsFeature");
            DropColumn("dbo.Cars", "Transmission");
        }
    }
}
