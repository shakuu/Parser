namespace Parser.Common.Logging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEntries",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 1000),
                        MessageType = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Action = c.String(nullable: false, maxLength: 200),
                        Controller = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogEntries");
        }
    }
}
