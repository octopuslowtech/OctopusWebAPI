using Microsoft.EntityFrameworkCore;

namespace OctopusWebAPI.Data
{
    public class MyOctpDBContext : DbContext
    {
        public MyOctpDBContext(DbContextOptions options) : base(options) { }

        public DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfo");
                entity.HasKey(e => e.UserName);
                entity.Property(entity => entity.UserName).HasMaxLength(50).IsRequired(); 
                entity.Property(entity => entity.Password).HasMaxLength(50).IsRequired();
            });

        }
    }
}
