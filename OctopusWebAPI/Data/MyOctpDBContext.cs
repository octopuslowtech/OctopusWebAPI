using Microsoft.EntityFrameworkCore;
using OctopusWebAPI.Entities;

namespace OctopusWebAPI.Data
{
    public class MyOctpDBContext : DbContext
    {
        public MyOctpDBContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTDS> AccountTDSs { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserID);
                entity.HasMany<Account>(p => p.Account).WithOne(p => p.User).HasForeignKey(p => p.UserID).OnDelete(DeleteBehavior.Cascade); // HasForeignKey("FK_USER_ACCOUNTTDS"
                entity.HasMany<AccountTDS>(p => p.AccountTDS).WithOne(p => p.User).HasForeignKey(p => p.UserID).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany<RefreshToken>(p => p.RefreshTokens).WithOne(p => p.User).HasForeignKey(p => p.UserID).OnDelete(DeleteBehavior.Cascade);
                entity.Property(entity => entity.UserID).HasMaxLength(50).IsRequired();
                entity.Property(entity => entity.Password).HasMaxLength(50).IsRequired();
            });
            builder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");
                entity.HasKey(e => e.UserTiktok);
                entity.Property(entity => entity.UserTiktok).HasMaxLength(250).IsRequired();
                entity.Property(entity => entity.Password).IsRequired();
            });
            builder.Entity<AccountTDS>(entity =>
            {
                entity.ToTable("AccountTDS");
                entity.HasKey(e => e.UserTDS);
                entity.Property(entity => entity.UserTDS).HasMaxLength(250).IsRequired();
                entity.Property(entity => entity.Password).IsRequired();
            });
            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");
                entity.HasKey(e => e.TokenId);
                entity.Property(entity => entity.Token).IsRequired();
            });
        }
    }
}
