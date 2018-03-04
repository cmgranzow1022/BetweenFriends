namespace BetweenFriends.Migrations.BFMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetDatabase : DbMigration
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
                "dbo.Customer_Address",
                c => new
                    {
                        Customer_AddressId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Customer_AddressId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        CellPhoneNumber = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Customer_Group",
                c => new
                    {
                        Customer_GroupId = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Customer_GroupId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        Date = c.String(),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        CustomerIdOne = c.Int(),
                        CustomerIdTwo = c.Int(),
                        FriendId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.FriendId)
                .ForeignKey("dbo.Customers", t => t.CustomerIdTwo)
                .ForeignKey("dbo.Customers", t => t.CustomerIdOne)
                .Index(t => t.CustomerIdOne)
                .Index(t => t.CustomerIdTwo);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vetoes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RestaurantSelections", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantSelections", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.PendingRequests", "CustomerIdOne", "dbo.Customers");
            DropForeignKey("dbo.PendingRequests", "CustomerIdTwo", "dbo.Customers");
            DropForeignKey("dbo.Friends", "CustomerIdOne", "dbo.Customers");
            DropForeignKey("dbo.Friends", "CustomerIdTwo", "dbo.Customers");
            DropForeignKey("dbo.Customer_Group", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Customer_Group", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customer_Address", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customer_Address", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Vetoes", new[] { "RestaurantId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RestaurantSelections", new[] { "GroupId" });
            DropIndex("dbo.RestaurantSelections", new[] { "RestaurantId" });
            DropIndex("dbo.PendingRequests", new[] { "CustomerIdTwo" });
            DropIndex("dbo.PendingRequests", new[] { "CustomerIdOne" });
            DropIndex("dbo.Friends", new[] { "CustomerIdTwo" });
            DropIndex("dbo.Friends", new[] { "CustomerIdOne" });
            DropIndex("dbo.Customer_Group", new[] { "CustomerId" });
            DropIndex("dbo.Customer_Group", new[] { "GroupId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Customer_Address", new[] { "AddressId" });
            DropIndex("dbo.Customer_Address", new[] { "CustomerId" });
            DropTable("dbo.Vetoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RestaurantSelections");
            DropTable("dbo.Restaurants");
            DropTable("dbo.PendingRequests");
            DropTable("dbo.Friends");
            DropTable("dbo.Groups");
            DropTable("dbo.Customer_Group");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Customers");
            DropTable("dbo.Customer_Address");
            DropTable("dbo.Addresses");
        }
    }
}
