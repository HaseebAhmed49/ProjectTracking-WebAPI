using System;
using Microsoft.EntityFrameworkCore;
using ProjectTracking_WebAPI.Models;

namespace ProjectTracking_WebAPI.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(em => em.EmoloyeeID);
        }

        public DbSet<UserStory> UserStory { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<ProjectTask> ProjectTask { get; set; }

        public DbSet<Employee> Employee { get; set; }
    }
}

