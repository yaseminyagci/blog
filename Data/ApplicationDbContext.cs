using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Enums;
using Core.Interfaces;
using Data.Configurations;
using Data.Entities;
using Data.Entities.Identity;
using Data.Entities.Public;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{

    //public class ApplicationDbContext : IdentityDbContext<User,
    //    Role, string, UserClaim,
    //    UserRole,
    //    UserLogin,
    //    RoleClaim,
    //    UserToken>

    public class ApplicationDbContext : IdentityDbContext<User,
        Role, string, UserClaim,
        UserRole,
        UserLogin,
        RoleClaim,
        UserToken>

    {

        #region Identity
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<PermissionCategory> PermissionCategories { get; set; }
        public DbSet<PermissionCategoryPermission> PermissionCategoryPermissions { get; set; }
        public DbSet<RolePermissionCategory> RolePermissionCategories { get; set; }
        #endregion


        //public DbSet<ButtonSettings> ButtonSettingses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<TagPost> TagPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<UserType> UserTypes { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RolePermissionCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            

            modelBuilder.Entity<User>().ToTable("Users", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<Role>().ToTable("Roles", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<UserToken>().ToTable("UserTokens", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<RolePermissionCategory>().ToTable("RolePermissionCategories", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<UserPermission>().ToTable("UserPermissions", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<Permission>().ToTable("Permissions", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<PermissionCategory>().ToTable("PermissionCategories", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.Entity<PermissionCategoryPermission>().ToTable("PermissionCategoryPermissions", GeneralConstants.IdentityTableSchemeName);
            modelBuilder.SetStatusQueryFilter();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["DateCreated"] = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["DateModified"] = DateTime.Now;
                        break;
                }
        }
    }
}
