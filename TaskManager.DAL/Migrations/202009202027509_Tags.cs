namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ticket", "Tags");
        }
    }
}
