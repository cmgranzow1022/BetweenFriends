namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatLongTable3 : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.GeoCoordinates");
        }
    }
}
