namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class File1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.File", "CommentId", "dbo.Comment");
            AddForeignKey("dbo.File", "CommentId", "dbo.Comment", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "CommentId", "dbo.Comment");
            AddForeignKey("dbo.File", "CommentId", "dbo.Comment", "Id");
        }
    }
}
