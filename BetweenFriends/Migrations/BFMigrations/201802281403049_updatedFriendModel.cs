namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedFriendModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        CustomerIdOne = c.Int(),
                        CustomerIdTwo = c.Int(),
                        FriendId = c.Int(nullable: false, identity: true),
                        Approve = c.Boolean(nullable: false),
                        Deny = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FriendId)
                .ForeignKey("dbo.Customers", t => t.CustomerIdTwo)
                .ForeignKey("dbo.Customers", t => t.CustomerIdOne)
                .Index(t => t.CustomerIdOne)
                .Index(t => t.CustomerIdTwo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "CustomerIdOne", "dbo.Customers");
            DropForeignKey("dbo.Friends", "CustomerIdTwo", "dbo.Customers");
            DropIndex("dbo.Friends", new[] { "CustomerIdTwo" });
            DropIndex("dbo.Friends", new[] { "CustomerIdOne" });
            DropTable("dbo.Friends");
        }
    }
}
