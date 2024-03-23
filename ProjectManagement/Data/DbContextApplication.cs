using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.Data
{
    public class DbContextApplication : DbContext
    {
        public DbContextApplication(DbContextOptions<DbContextApplication> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<ProjectComment>().ToTable("ProjectComment");

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Comments)
                .WithOne(pc => pc.Project)
                .HasForeignKey(pc => pc.ProjectId);
        }
    }
}
