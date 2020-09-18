namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateModified = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        TicketId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.TicketId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Comment", "TicketId", "dbo.Ticket");
            DropIndex("dbo.Comment", new[] { "EmployeeId" });
            DropIndex("dbo.Comment", new[] { "TicketId" });
            DropTable("dbo.Comment");
        }
    }
}
