namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(maxLength: 2),
                        ZipCode = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        CellPhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
