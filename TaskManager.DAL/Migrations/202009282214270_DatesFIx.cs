namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatesFIx : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ticket", "ExecutionDate", c => c.DateTime());
            AlterColumn("dbo.Ticket", "ReceiptDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ticket", "ReceiptDate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Ticket", "ExecutionDate", c => c.DateTimeOffset(precision: 7));
        }
    }
}
