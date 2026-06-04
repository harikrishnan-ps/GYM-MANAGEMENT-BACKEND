using GymManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GymManagement.Api.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<GymClass> GymClasses { get; set; }
        public DbSet<Revenue> Revenues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Email uniqueness
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Email)
                .IsUnique();

            // Seed Admin User
            // Password: Admin@123
            // Hash: $2a$11$7Iyr51qOVNqkf.VDHvaBku4bQNzUNpHa5mQaa6fuD9prndm1WYPLW
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    Name = "System Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = "$2a$11$7Iyr51qOVNqkf.VDHvaBku4bQNzUNpHa5mQaa6fuD9prndm1WYPLW",
                    Role = "Admin",
                    CreatedAt = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
