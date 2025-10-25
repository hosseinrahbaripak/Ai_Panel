
using Ai_Panel.Domain;
using Ai_Panel.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Ai_Panel.Persistence.Configurations
{
    public class AiPanelContext : DbContext
    {
        public AiPanelContext(DbContextOptions<AiPanelContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ApiRequestLog> ApiRequestLogs { get; set; }
        public DbSet<TestAiConfig> TestAiConfigs { get; set; }
        public DbSet<UserAiChatLog> UserAiChatLogs { get; set; }
        public DbSet<AiConfig> AiConfigs { get; set; }
        public DbSet<AiModel> AiModels { get; set; }
        public DbSet<AiPlatform> AiPlatforms { get; set; }
        public DbSet<AiService> AiServices { get; set; }
        public DbSet<UserService> UserServices { get; set; }
        public DbSet<AiConfigGroup> AiConfigGroups { get; set; }
        public DbSet<ContractTemplate> ContractTemplates { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(AiPanelContext).Assembly);


            modelBuilder.Entity<User>().HasQueryFilter(x => x.IsDelete == false);
            modelBuilder.Entity<UserSession>().HasQueryFilter(x => x.Users.IsDelete == false);
            modelBuilder.Entity<UserAiChatLog>()
                .HasOne(c => c.UserService)
                .WithMany(u => u.UserAiChatLogs)
                .HasForeignKey(c => c.UserServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles) 
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdateDateTime = DateTime.UtcNow.AddHours(3.5);

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.IsDelete = false;
                    entry.Entity.DateTime = DateTime.UtcNow.AddHours(3.5);
                }
            }


            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdateDateTime = DateTime.UtcNow.AddHours(3.5);

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.IsDelete = false;
                    entry.Entity.DateTime = DateTime.UtcNow.AddHours(3.5);
                }
            }
            return base.SaveChanges();
        }
        #endregion

    }
}
