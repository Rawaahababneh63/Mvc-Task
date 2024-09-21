using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using MiniSchool.Models;


namespace MiniSchool.Models
{
    public class MiniSchoolContext : DbContext

    {

        public MiniSchoolContext() : base("name=MiniSchoolEntities")
        {
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<User> Users { get; set; } 


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configure relationships
            modelBuilder.Entity<Student>()
                .HasRequired(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId);

            modelBuilder.Entity<Assignment>()
                .HasRequired(a => a.Class)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.ClassId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
