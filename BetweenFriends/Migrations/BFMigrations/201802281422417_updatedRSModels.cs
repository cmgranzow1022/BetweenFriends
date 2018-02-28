namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedRSModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Groups", new[] { "RestaurantId" });
            CreateTable(
                "dbo.RestaurantSelections",
                c => new
                    {
                        RestaurantSelectionId = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantSelectionId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Vetoes",
                c => new
                    {
                        VetoId = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.VetoId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            AlterColumn("dbo.Groups", "RestaurantId", c => c.Int());
            CreateIndex("dbo.Groups", "RestaurantId");
            AddForeignKey("dbo.Groups", "RestaurantId", "dbo.Restaurants", "RestaurantId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Vetoes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantSelections", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantSelections", "GroupId", "dbo.Groups");
            DropIndex("dbo.Vetoes", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantSelections", new[] { "GroupId" });
            DropIndex("dbo.RestaurantSelections", new[] { "RestaurantId" });
            DropIndex("dbo.Groups", new[] { "RestaurantId" });
            AlterColumn("dbo.Groups", "RestaurantId", c => c.Int(nullable: false));
            DropTable("dbo.Vetoes");
            DropTable("dbo.RestaurantSelections");
            CreateIndex("dbo.Groups", "RestaurantId");
            AddForeignKey("dbo.Groups", "RestaurantId", "dbo.Restaurants", "RestaurantId", cascadeDelete: true);
        }
    }
}
