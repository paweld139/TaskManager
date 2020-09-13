namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employee", "CreateDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "CreateDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
