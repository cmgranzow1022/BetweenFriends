namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPendingRequestTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Friends", "Approve");
            DropColumn("dbo.Friends", "Deny");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "Deny", c => c.Boolean(nullable: false));
            AddColumn("dbo.Friends", "Approve", c => c.Boolean(nullable: false));
        }
    }
}
