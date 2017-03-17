namespace Parser.Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedhealing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoredCombatStatistics", "HealingDone", c => c.Double(nullable: false));
            AddColumn("dbo.StoredCombatStatistics", "HealingDonePerSecond", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoredCombatStatistics", "HealingDonePerSecond");
            DropColumn("dbo.StoredCombatStatistics", "HealingDone");
        }
    }
}
