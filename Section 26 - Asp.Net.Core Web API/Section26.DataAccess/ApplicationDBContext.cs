using Microsoft.EntityFrameworkCore;
using Section26.DataAccess.Entities;

namespace Section26.DataAccess
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
