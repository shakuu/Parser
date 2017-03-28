using System.Data.Entity;

using Parser.Auth;
using Parser.Common.Logging;

namespace Parser.MvcClient
{
    public class DatabaseConfig
    {
        public static void InitializeDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthDbContext, Parser.Auth.Migrations.Configuration>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LoggingServiceDbContext, Parser.Common.Logging.Migrations.Configuration>());
        }
    }
}