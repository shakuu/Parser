namespace Parser.Auth.Migrations
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
                        ParserUserId = c.Guid(),
                        CharacterName = c.String(nullable: false, maxLength: 50),
                        CombatDurationInSeconds = c.Double(nullable: false),
                        DamageDone = c.Double(nullable: false),
                        DamageDonePerSecond = c.Double(nullable: false),
                        DamageTaken = c.Double(nullable: false),
                        DamageTakenPerSecond = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParserUsers", t => t.ParserUserId)
                .Index(t => t.ParserUserId);
            
            AddColumn("dbo.AspNetUsers", "ParserUserId", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "ParserUserId");
            AddForeignKey("dbo.AspNetUsers", "ParserUserId", "dbo.ParserUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ParserUserId", "dbo.ParserUsers");
            DropForeignKey("dbo.StoredCombatStatistics", "ParserUserId", "dbo.ParserUsers");
            DropIndex("dbo.StoredCombatStatistics", new[] { "ParserUserId" });
            DropIndex("dbo.AspNetUsers", new[] { "ParserUserId" });
            DropColumn("dbo.AspNetUsers", "ParserUserId");
            DropTable("dbo.StoredCombatStatistics");
            DropTable("dbo.ParserUsers");
        }
    }
}
