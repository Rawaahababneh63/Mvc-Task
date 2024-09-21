namespace MiniSchool.Migrations
{
    using MiniSchool.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MiniSchool.Models.MiniSchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MiniSchool.Models.MiniSchoolContext context)
        {
            context.Subjects.AddOrUpdate(
             s => s.Name, // This is a unique property to avoid duplicates
             new Subject { Name = "Mathematics" },
             new Subject { Name = "Science" },
             new Subject { Name = "English" },
             new Subject { Name = "History" },
             new Subject { Name = "Arabic" });
        }
    }
}
