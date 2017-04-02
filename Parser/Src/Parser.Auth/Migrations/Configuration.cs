using System.Data.Entity.Migrations;
using System.Linq;

using Microsoft.AspNet.Identity.EntityFramework;

namespace Parser.Auth.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Parser.Auth.AuthDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //ContextKey = "Parser.Auth.AuthDbContext";
        }

        protected override void Seed(Parser.Auth.AuthDbContext context)
        {
            if (!context.Roles.Any())
            {
                var adminRole = new IdentityRole("Admin");

                context.Roles.Add(adminRole);

                context.SaveChanges();
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
