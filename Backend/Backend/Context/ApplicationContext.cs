using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<AssessmentDatabaseModel> Assessments { get; set; }
        public DbSet<BasketDatabaseModel> Baskets { get; set; }
        public DbSet<BasketProductDatabaseModel> BasketProducts { get; set; }
        public DbSet<CommentDatabaseModel> Comments { get; set; }
        public DbSet<OrderDatabaseModel> Orders { get; set; }
        public DbSet<OrderProductDatabaseModel> OrderProducts { get; set; }
        public DbSet<ProductDatabaseModel> Products { get; set; }
        public DbSet<ProductInformationDatabaseModel> ProductInformations { get; set; }
        public DbSet<ProviderDatabaseModel> Providers { get; set; }
        public DbSet<RoleDatabaseModel> Roles { get; set; }
        public DbSet<UserDatabaseModel> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> operations) 
            : base(operations)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssessmentConfiguration());
            modelBuilder.ApplyConfiguration(new BasketConfiguration());
            modelBuilder.ApplyConfiguration(new BasketProductConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInformationConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}