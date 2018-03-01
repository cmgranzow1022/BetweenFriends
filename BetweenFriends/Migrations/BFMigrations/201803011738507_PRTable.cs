namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PendingRequests",
                c => new
                    {
                        CustomerIdOne = c.Int(),
                        CustomerIdTwo = c.Int(),
                        PendingRequestId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PendingRequestId)
                .ForeignKey("dbo.Customers", t => t.CustomerIdTwo)
                .ForeignKey("dbo.Customers", t => t.CustomerIdOne)
                .Index(t => t.CustomerIdOne)
                .Index(t => t.CustomerIdTwo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PendingRequests", "CustomerIdOne", "dbo.Customers");
            DropForeignKey("dbo.PendingRequests", "CustomerIdTwo", "dbo.Customers");
            DropIndex("dbo.PendingRequests", new[] { "CustomerIdTwo" });
            DropIndex("dbo.PendingRequests", new[] { "CustomerIdOne" });
            DropTable("dbo.PendingRequests");
        }
    }
}
