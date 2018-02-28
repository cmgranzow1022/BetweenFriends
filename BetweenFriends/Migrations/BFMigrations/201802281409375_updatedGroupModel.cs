namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedGroupModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        CustomerOwnerId = c.Int(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                        Date = c.String(),
                        Time = c.String(),
                        Attending = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Customers", t => t.CustomerOwnerId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.CustomerOwnerId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        Cuisine = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Groups", "CustomerOwnerId", "dbo.Customers");
            DropIndex("dbo.Groups", new[] { "RestaurantId" });
            DropIndex("dbo.Groups", new[] { "CustomerOwnerId" });
            DropTable("dbo.Restaurants");
            DropTable("dbo.Groups");
        }
    }
}
