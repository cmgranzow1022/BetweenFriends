namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Groups", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Groups", new[] { "CustomerId" });
            DropIndex("dbo.Groups", new[] { "RestaurantId" });
            DropColumn("dbo.Groups", "CustomerId");
            DropColumn("dbo.Groups", "RestaurantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "RestaurantId", c => c.Int());
            AddColumn("dbo.Groups", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Groups", "RestaurantId");
            CreateIndex("dbo.Groups", "CustomerId");
            AddForeignKey("dbo.Groups", "RestaurantId", "dbo.Restaurants", "RestaurantId");
            AddForeignKey("dbo.Groups", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
