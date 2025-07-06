using Microsoft.EntityFrameworkCore;
using Section27.DataAccess.Entities;

namespace Section27.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Master_User> Users { get; set; }
        public ApplicationDBContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Master_User>()
                .ToTable("Users");

            modelBuilder.Entity<Master_User>()
                .Property(x => x.RecordID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Master_User>()
                .HasIndex(x => x.Email).IsUnique();

        }

    }
}
