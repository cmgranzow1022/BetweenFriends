namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedgeoC : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.GeoCoordinates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GeoCoordinates",
                c => new
                    {
                        latLongId = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.latLongId);
            
        }
    }
}
