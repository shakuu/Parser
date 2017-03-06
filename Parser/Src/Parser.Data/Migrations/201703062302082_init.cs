namespace Parser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParserUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Username = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoredCombatStatistics",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        ParserUserId = c.Guid(nullable: false),
                        CharacterName = c.String(nullable: false, maxLength: 50),
                        CombatDurationInSeconds = c.Double(nullable: false),
                        DamageDone = c.Double(nullable: false),
                        DamageDonePerSecond = c.Double(nullable: false),
                        DamageTaken = c.Double(nullable: false),
                        DamageTakenPerSecond = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParserUsers", t => t.ParserUserId, cascadeDelete: true)
                .Index(t => t.ParserUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoredCombatStatistics", "ParserUserId", "dbo.ParserUsers");
            DropIndex("dbo.StoredCombatStatistics", new[] { "ParserUserId" });
            DropTable("dbo.StoredCombatStatistics");
            DropTable("dbo.ParserUsers");
        }
    }
}
