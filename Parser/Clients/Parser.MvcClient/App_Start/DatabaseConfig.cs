using System.Data.Entity.Migrations;

namespace Parser.MvcClient
{
    public class DatabaseConfig
    {
        public static void InitializeDatabase()
        {
            var authDbContextMigrator = new DbMigrator(new Parser.Auth.Migrations.Configuration());
            authDbContextMigrator.Update();

            var loggingServiceDbContextMigrator = new DbMigrator(new Parser.Common.Logging.Migrations.Configuration());
            loggingServiceDbContextMigrator.Update();
        }
    }
}