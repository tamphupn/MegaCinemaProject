namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Birthday = c.DateTime(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        SSN = c.String(nullable: false, maxLength: 12),
                        Phone = c.String(nullable: false, maxLength: 12),
                        Address = c.String(nullable: false, maxLength: 100),
                        District = c.String(maxLength: 100),
                        City = c.String(maxLength: 100),
                        Avatar = c.String(maxLength: 100),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        Customer_CustomerID = c.Int(),
                        Staff_StaffID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID)
                .ForeignKey("dbo.Staffs", t => t.Staff_StaffID)
                .Index(t => t.Customer_CustomerID)
                .Index(t => t.Staff_StaffID);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.ApplicationUserRoles", "IdentityRole_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ApplicationUserRoles", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ApplicationUserLogins", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ApplicationUserRoles", "IdentityRole_Id");
            CreateIndex("dbo.ApplicationUserRoles", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserLogins", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.IdentityRoles", "Id");
            AddForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUsers", "Staff_StaffID", "dbo.Staffs");
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUsers", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropIndex("dbo.ApplicationUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUsers", new[] { "Staff_StaffID" });
            DropIndex("dbo.ApplicationUsers", new[] { "Customer_CustomerID" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropColumn("dbo.ApplicationUserLogins", "ApplicationUser_Id");
            DropColumn("dbo.ApplicationUserRoles", "ApplicationUser_Id");
            DropColumn("dbo.ApplicationUserRoles", "IdentityRole_Id");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.IdentityRoles");
        }
    }
}
