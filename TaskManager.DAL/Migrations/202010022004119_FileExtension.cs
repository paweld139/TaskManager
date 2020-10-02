namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileExtension : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.File", "Extension", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.File", "Extension", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
