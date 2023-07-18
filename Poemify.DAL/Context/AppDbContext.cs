using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Poemify.Models.Entities;

namespace Poemify.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string,
        UserClaim, UserRole, UserLogin, RoleClaim,
        UserToken>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<AppUser> Users { get; set; }
        DbSet<Poem> Poems { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            base.OnModelCreating(builder);
            builder.Entity<Tag>().HasIndex(t => t.Name).IsUnique();
            builder.Entity<AppUser>().HasIndex(u => u.UserName).IsUnique();



            builder.Entity<AppUser>(b =>
            {

                b.HasKey(u => u.Id);


                b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");


                b.ToTable("AspNetUsers");


                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();


                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();


                b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();


                b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<UserClaim>(b =>
            {

                b.HasKey(uc => uc.Id);


                b.ToTable("AspNetUserClaims");
            });

            builder.Entity<UserLogin>(b =>
            {

                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });


                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                b.ToTable("AspNetUserLogins");
            });

            builder.Entity<UserToken>(b =>
            {

                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                b.Property(t => t.LoginProvider).HasMaxLength(50);
                b.Property(t => t.Name).HasMaxLength(50);


                b.ToTable("AspNetUserTokens");
            });

            builder.Entity<AppRole>(b =>
            {

                b.HasKey(r => r.Id);


                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();


                b.ToTable("AspNetRoles");

                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);


                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();


                b.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Entity<RoleClaim>(b =>
            {

                b.HasKey(rc => rc.Id);

                b.ToTable("AspNetRoleClaims");
            });

            builder.Entity<UserRole>(b =>
            {//./
                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                b.HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique()
                .IsClustered(false);


                b.ToTable("AspNetUserRoles");
            });

        }

    }
}
