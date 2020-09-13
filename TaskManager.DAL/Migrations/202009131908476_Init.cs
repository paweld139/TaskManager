namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contrahent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Archived = c.Boolean(nullable: false),
                        NIP = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 150),
                        IsOperator = c.Boolean(nullable: false),
                        LicenseKey = c.String(nullable: false, maxLength: 20),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false),
                        Number = c.String(nullable: false, maxLength: 50),
                        TypeId = c.Int(nullable: false),
                        PriorityId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        RepresentativeId = c.Int(nullable: false),
                        OperatorId = c.Int(),
                        ContrahentId = c.Int(nullable: false),
                        Budget = c.Decimal(precision: 18, scale: 2),
                        ExecutionDate = c.DateTimeOffset(precision: 7),
                        ReceiptDate = c.DateTimeOffset(precision: 7),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contrahent", t => t.ContrahentId)
                .ForeignKey("dbo.Employee", t => t.OperatorId)
                .ForeignKey("dbo.Dictionary", t => t.PriorityId)
                .ForeignKey("dbo.Employee", t => t.RepresentativeId, cascadeDelete: true)
                .ForeignKey("dbo.Dictionary", t => t.StatusId)
                .ForeignKey("dbo.Dictionary", t => t.TypeId)
                .Index(t => t.TypeId)
                .Index(t => t.PriorityId)
                .Index(t => t.StatusId)
                .Index(t => t.RepresentativeId)
                .Index(t => t.OperatorId)
                .Index(t => t.ContrahentId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContrahentId = c.Int(nullable: false),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contrahent", t => t.ContrahentId, cascadeDelete: true)
                .Index(t => t.ContrahentId);
            
            CreateTable(
                "dbo.Dictionary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Value = c.String(nullable: false, maxLength: 150),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        ELId = c.Int(nullable: false, identity: true),
                        ErrorType = c.String(),
                        Message = c.String(),
                        ErrorMessage = c.String(),
                        StackTrace = c.String(),
                        LogLevel = c.Int(nullable: false),
                        RequestUri = c.String(),
                        MachineName = c.String(),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ELId);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        ALLFId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Extension = c.String(),
                        RefId = c.Int(nullable: false),
                        RefGid = c.Int(nullable: false),
                        Description = c.String(),
                        GroupId = c.Int(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ALLFId);
            
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
                "dbo.UserData",
                c => new
                    {
                        ULId = c.Int(nullable: false, identity: true),
                        IP = c.String(),
                        OperatingSystem = c.String(),
                        Device = c.Int(nullable: false),
                        PhoneModel = c.String(),
                        PhoneManufacturer = c.String(),
                        Platform = c.String(),
                        Resolution = c.String(),
                        Language = c.String(),
                        Browser = c.String(),
                        ServiceUnresponded = c.Boolean(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Type = c.String(),
                        ContinentCode = c.String(),
                        ContinentName = c.String(),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                        RegionCode = c.String(),
                        RegionName = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        LocationId = c.Int(),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.ULId)
                .ForeignKey("dbo.Location", t => t.LocationId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LOId = c.Int(nullable: false, identity: true),
                        GeonameId = c.Int(),
                        Capital = c.String(),
                        CountryFlag = c.String(),
                        CountryFlagEmoji = c.String(),
                        CountryFlagEmojiUnicode = c.String(),
                        CallingCode = c.String(),
                        IsEu = c.Boolean(),
                    })
                .PrimaryKey(t => t.LOId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LAId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Native = c.String(),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LAId)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Hometown = c.String(),
                        EmployeeId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
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
            DropForeignKey("dbo.AspNetUsers", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserData", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Language", "LocationId", "dbo.Location");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ticket", "TypeId", "dbo.Dictionary");
            DropForeignKey("dbo.Ticket", "StatusId", "dbo.Dictionary");
            DropForeignKey("dbo.Ticket", "RepresentativeId", "dbo.Employee");
            DropForeignKey("dbo.Ticket", "PriorityId", "dbo.Dictionary");
            DropForeignKey("dbo.Ticket", "OperatorId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "ContrahentId", "dbo.Contrahent");
            DropForeignKey("dbo.Ticket", "ContrahentId", "dbo.Contrahent");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "EmployeeId" });
            DropIndex("dbo.Language", new[] { "LocationId" });
            DropIndex("dbo.UserData", new[] { "LocationId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Employee", new[] { "ContrahentId" });
            DropIndex("dbo.Ticket", new[] { "ContrahentId" });
            DropIndex("dbo.Ticket", new[] { "OperatorId" });
            DropIndex("dbo.Ticket", new[] { "RepresentativeId" });
            DropIndex("dbo.Ticket", new[] { "StatusId" });
            DropIndex("dbo.Ticket", new[] { "PriorityId" });
            DropIndex("dbo.Ticket", new[] { "TypeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Language");
            DropTable("dbo.Location");
            DropTable("dbo.UserData");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.File");
            DropTable("dbo.Log");
            DropTable("dbo.Dictionary");
            DropTable("dbo.Employee");
            DropTable("dbo.Ticket");
            DropTable("dbo.Contrahent");
        }
    }
}
