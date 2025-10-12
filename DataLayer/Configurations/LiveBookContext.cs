using Live_Book.Domain;
using Live_Book.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Live_Book.Persistence.Configurations
{
    public class LiveBookContext : DbContext
    {
        public LiveBookContext(DbContextOptions<LiveBookContext> options) : base(options)
        {

        }
        public DbSet<AdminLogin> AdminLogins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Pages> Pages { get; set; }
        public DbSet<RolesInPages> RolesInPages { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<RequestLogin> RequestLogins { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ApiRequestLog> ApiRequestLogs { get; set; }
        //public DbSet<UserLikeOnBookPart> UserLikeOnBookParts { get; set; }
        public DbSet<UserAiChatLog> UserAiChatLogs { get; set; }
        public DbSet<AdminType> AdminTypes { get; set; }
        public DbSet<AiConfig> AiConfigs { get; set; }
        public DbSet<AiModel> AiModels { get; set; }
        public DbSet<AiContent> AiContents { get; set; }
        public DbSet<TestAiConfig> TestAiConfigs { get; set; }
        public DbSet<AiPlatform> AiPlatforms { get; set; }
        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(LiveBookContext).Assembly);

            modelBuilder
                .Entity<TestAiConfig>()
                .HasOne(e => e.AiModel)
                .WithMany(x => x.TestAiConfigs)
                .OnDelete(DeleteBehavior.ClientCascade);

           

            modelBuilder.Entity<User>().HasQueryFilter(x => x.IsDelete == false);
            modelBuilder.Entity<UserSession>().HasQueryFilter(x => x.Users.IsDelete == false);
            modelBuilder.Entity<AdminLogin>().HasQueryFilter(x => x.IsDelete == false);
            //modelBuilder.Entity<RolesInPages>().HasQueryFilter(x => x.Role.AdminRoles == false);

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
