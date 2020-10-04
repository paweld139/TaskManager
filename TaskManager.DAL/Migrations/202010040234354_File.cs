namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class File : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.File");
            DropColumn("dbo.File", "ALLFId");
            AddColumn("dbo.File", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.File", "MimeType", c => c.String(maxLength: 100));
            AddColumn("dbo.File", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.File", "CommentId", c => c.Int());
            AddColumn("dbo.File", "TicketId", c => c.Int());
            AddColumn("dbo.File", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.File", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.File", "Extension", c => c.String(maxLength: 20));
            AddPrimaryKey("dbo.File", "Id");
            CreateIndex("dbo.File", "UserId");
            CreateIndex("dbo.File", "CommentId");
            CreateIndex("dbo.File", "TicketId");
            AddForeignKey("dbo.File", "CommentId", "dbo.Comment", "Id");
            AddForeignKey("dbo.File", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.File", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.File");
            DropColumn("dbo.File", "Id");
            AddColumn("dbo.File", "ALLFId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.File", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.File", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.File", "CommentId", "dbo.Comment");
            DropIndex("dbo.File", new[] { "TicketId" });
            DropIndex("dbo.File", new[] { "CommentId" });
            DropIndex("dbo.File", new[] { "UserId" });
            AlterColumn("dbo.File", "Extension", c => c.String());
            AlterColumn("dbo.File", "Name", c => c.String());
            DropColumn("dbo.File", "Discriminator");
            DropColumn("dbo.File", "TicketId");
            DropColumn("dbo.File", "CommentId");
            DropColumn("dbo.File", "UserId");
            DropColumn("dbo.File", "MimeType");
            AddPrimaryKey("dbo.File", "ALLFId");
        }
    }
}
