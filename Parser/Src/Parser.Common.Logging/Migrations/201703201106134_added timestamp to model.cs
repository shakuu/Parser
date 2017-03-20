namespace Parser.Common.Logging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtimestamptomodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogEntries", "Timestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogEntries", "Timestamp");
        }
    }
}
