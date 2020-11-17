using Microsoft.EntityFrameworkCore;
using ProjectManagementCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Data
{
    public class PmcAppDbContext : DbContext 
    {

        public PmcAppDbContext(DbContextOptions<PmcAppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<FactorSubCategory> FactorSubCategories { get; set; }

        public DbSet<FactorMainCategory> FactorMainCategories { get; set; }

        public DbSet<Factor> Factors { get; set; }
        
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
