namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberFIx1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ticket", "Number", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ticket", "Number", c => c.String(nullable: false));
        }
    }
}
