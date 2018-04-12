namespace JumboBossWorkFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20, unicode: false),
                        ActionName = c.String(maxLength: 100, unicode: false),
                        ControllerName = c.String(maxLength: 100, unicode: false),
                        About = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        userInfo_Name = c.String(nullable: false, maxLength: 25, unicode: false),
                        userInfo_SurName = c.String(nullable: false, maxLength: 25, unicode: false),
                        userInfo_About = c.String(maxLength: 350, unicode: false),
                        userInfo_CompanyInfo = c.String(maxLength: 350, unicode: false),
                        userInfo_Address = c.String(maxLength: 350, unicode: false),
                        userInfo_ProfilPicture = c.String(maxLength: 8000, unicode: false),
                        userInfo_Department = c.String(maxLength: 25, unicode: false),
                        userInfo_CreatedOn = c.DateTime(nullable: false),
                        userInfo_DependencyId = c.String(),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WorkCommenteds",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Commented = c.String(nullable: false, maxLength: 300, unicode: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Workflow_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Workflows", t => t.Workflow_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Workflow_Id);
            
            CreateTable(
                "dbo.Workflows",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RequestingUserId = c.String(nullable: false, maxLength: 128),
                        LikeCount = c.Int(nullable: false),
                        EmployeeUsers_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeUsers_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestingUserId)
                .Index(t => t.RequestingUserId)
                .Index(t => t.EmployeeUsers_Id);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(nullable: false),
                        RequestingUserId = c.String(nullable: false, maxLength: 128),
                        WorkDescription = c.String(nullable: false, maxLength: 300, unicode: false),
                        WorkTitle = c.String(nullable: false, maxLength: 300, unicode: false),
                        EmployeeUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestingUserId)
                .Index(t => t.RequestingUserId)
                .Index(t => t.EmployeeUser_Id);
            
            CreateTable(
                "dbo.WorkAdditions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Addition = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Work_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Works", t => t.Work_Id)
                .Index(t => t.Work_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "RequestingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Works", "EmployeeUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkAdditions", "Work_Id", "dbo.Works");
            DropForeignKey("dbo.WorkCommenteds", "Workflow_Id", "dbo.Workflows");
            DropForeignKey("dbo.Workflows", "RequestingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Workflows", "EmployeeUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkCommenteds", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.WorkAdditions", new[] { "Work_Id" });
            DropIndex("dbo.Works", new[] { "EmployeeUser_Id" });
            DropIndex("dbo.Works", new[] { "RequestingUserId" });
            DropIndex("dbo.Workflows", new[] { "EmployeeUsers_Id" });
            DropIndex("dbo.Workflows", new[] { "RequestingUserId" });
            DropIndex("dbo.WorkCommenteds", new[] { "Workflow_Id" });
            DropIndex("dbo.WorkCommenteds", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.WorkAdditions");
            DropTable("dbo.Works");
            DropTable("dbo.Workflows");
            DropTable("dbo.WorkCommenteds");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Logs");
        }
    }
}
