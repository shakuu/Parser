namespace Parser.Common.Logging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmoreprops : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogEntries", "Method", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.LogEntries", "Controller", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogEntries", "Controller");
            DropColumn("dbo.LogEntries", "Method");
        }
    }
}
