namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Long(nullable: false, identity: true),
                        AuthorName = c.String(maxLength: 250),
                        History = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        CateId = c.Long(),
                        AuthorId = c.Long(),
                        PubId = c.Long(),
                        Summary = c.String(maxLength: 250),
                        ImgUrl = c.String(maxLength: 250),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ViewCount = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.Category", t => t.CateId)
                .ForeignKey("dbo.Publisher", t => t.PubId)
                .Index(t => t.CateId)
                .Index(t => t.AuthorId)
                .Index(t => t.PubId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CateId = c.Long(nullable: false, identity: true),
                        CateName = c.String(maxLength: 250),
                        Desciption = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.CateId);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        PubId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.PubId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Commentld = c.Long(nullable: false, identity: true),
                        BookId = c.Long(),
                        Content = c.String(storeType: "ntext"),
                        CreatedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Commentld)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.BookId);
            
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
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comment", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "PubId", "dbo.Publisher");
            DropForeignKey("dbo.Book", "CateId", "dbo.Category");
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Comment", new[] { "BookId" });
            DropIndex("dbo.Book", new[] { "PubId" });
            DropIndex("dbo.Book", new[] { "AuthorId" });
            DropIndex("dbo.Book", new[] { "CateId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Comment");
            DropTable("dbo.Publisher");
            DropTable("dbo.Category");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
