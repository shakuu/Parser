namespace Parser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class optionaluser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoredCombatStatistics", "ParserUserId", "dbo.ParserUsers");
            DropIndex("dbo.StoredCombatStatistics", new[] { "ParserUserId" });
            AlterColumn("dbo.StoredCombatStatistics", "ParserUserId", c => c.Guid());
            CreateIndex("dbo.StoredCombatStatistics", "ParserUserId");
            AddForeignKey("dbo.StoredCombatStatistics", "ParserUserId", "dbo.ParserUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoredCombatStatistics", "ParserUserId", "dbo.ParserUsers");
            DropIndex("dbo.StoredCombatStatistics", new[] { "ParserUserId" });
            AlterColumn("dbo.StoredCombatStatistics", "ParserUserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StoredCombatStatistics", "ParserUserId");
            AddForeignKey("dbo.StoredCombatStatistics", "ParserUserId", "dbo.ParserUsers", "Id", cascadeDelete: true);
        }
    }
}
