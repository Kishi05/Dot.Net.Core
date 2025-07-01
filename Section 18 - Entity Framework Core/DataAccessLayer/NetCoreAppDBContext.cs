using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class NetCoreAppDBContext : DbContext
    {
        public NetCoreAppDBContext(DbContextOptions options):base(options)
        {
                
        }
        public DbSet<Admin_User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Map Model Class to Table
            modelBuilder.Entity<Admin_User>().ToTable("User");

            //Seed Data
            modelBuilder.Entity<Admin_User>().HasData(
                new Admin_User
                {
                    Id = 1,
                    Name = "Test User",
                    Location = "UK",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    Email = "testuser@netcore.com",
                    isDummy = true
                });
        }

    }
}
