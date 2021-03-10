using Microsoft.EntityFrameworkCore;
using ProjectManagementCollection.Models;

namespace ProjectManagementCollection.Data
{
    public class PmcAppDbContext : DbContext
    {

        public PmcAppDbContext(DbContextOptions<PmcAppDbContext> options) : base(options)
        {

        }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<FactorSubCategory> FactorSubCategories { get; set; }

        public DbSet<FactorMainCategory> FactorMainCategories { get; set; }

        public DbSet<Factor> Factors { get; set; }

        public DbSet<DocumentFactorRel> DocumentFactorRels{ get; set; }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasIndex(m => m.Level)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<Document>()
                .HasOne(m => m.Project)
                .WithMany(n => n.Documents);

            base.OnModelCreating(modelBuilder);
        }
    }
}
