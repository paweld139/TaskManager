namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class File : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "MimeType", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.File", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.File", "Name", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.File", "Extension", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.File", "UserId");
            AddForeignKey("dbo.File", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.File", new[] { "UserId" });
            AlterColumn("dbo.File", "Extension", c => c.String());
            AlterColumn("dbo.File", "Name", c => c.String());
            DropColumn("dbo.File", "UserId");
            DropColumn("dbo.File", "MimeType");
        }
    }
}
