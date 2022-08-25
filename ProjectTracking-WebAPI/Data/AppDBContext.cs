using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectTracking_WebAPI.Data.Models;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data
{
    public class AppDBContext: IdentityDbContext<Users>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(em => em.EmployeeID);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserStory> UserStory { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<ProjectTask> ProjectTask { get; set; }

        public DbSet<Employee> Employee { get; set; }

        // JWT Refresh
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}

