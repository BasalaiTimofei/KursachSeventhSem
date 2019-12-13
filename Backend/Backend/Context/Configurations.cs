using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Context
{
    public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.User)
                .WithMany(w => w.Assessments)
                .HasForeignKey(w => w.UserId);
            builder.HasOne(w => w.Product)
                .WithMany(w => w.Assessments)
                .HasForeignKey(w => w.ProductId);

            builder.Property(w => w.Value).IsRequired();
        }
    }

    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.User)
                .WithOne(w => w.Basket);
            builder.HasMany(w => w.Products)
                .WithOne(w => w.Basket);
        }
    }

    public class BasketProductConfiguration : IEntityTypeConfiguration<BasketProduct>
    {
        public void Configure(EntityTypeBuilder<BasketProduct> builder)
        {
            builder.HasKey(w => new {w.BasketId, w.ProductId});

            builder.HasOne(w => w.Basket)
                .WithMany(w => w.Products)
                .HasForeignKey(w => w.BasketId);
            builder.HasOne(w => w.Product)
                .WithMany(w => w.Baskets)
                .HasForeignKey(w => w.ProductId);
        }
    }

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.User)
                .WithMany(w => w.Comments)
                .HasForeignKey(w => w.UserId);
            builder.HasOne(w => w.Product)
                .WithMany(w => w.Comments)
                .HasForeignKey(w => w.ProductId);

            builder.Property(w => w.Text).IsRequired().HasMaxLength(1000);
            builder.Property(w => w.DateTimeCreate).IsRequired();
        }
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.User)
                .WithMany(w => w.Orders)
                .HasForeignKey(w => w.UserId);
            builder.HasMany(w => w.Products)
                .WithOne(w => w.Order);

            builder.Property(w => w.DateTimeCreate).IsRequired();
        }
    }

    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(w => new {w.OrderId, w.ProductId});

            builder.HasOne(w => w.Order)
                .WithMany(w => w.Products)
                .HasForeignKey(w => w.OrderId);
            builder.HasOne(w => w.Product)
                .WithMany(w => w.Orders)
                .HasForeignKey(w => w.ProductId);
        }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.ProductInformation)
                .WithOne(w => w.Product)
                .HasForeignKey<ProductInformation>(w => w.ProductId);
            builder.HasOne(w => w.Provider)
                .WithMany(w => w.Products)
                .HasForeignKey(w => w.ProviderId);
            builder.HasMany(w => w.Assessments)
                .WithOne(w => w.Product);
            builder.HasMany(w => w.Baskets)
                .WithOne(w => w.Product);
            builder.HasMany(w => w.Orders)
                .WithOne(w => w.Product);
            builder.HasMany(w => w.Comments)
                .WithOne(w => w.Product);

            builder.Property(w => w.Name).IsRequired().HasMaxLength(100);
            builder.Property(w => w.Price).IsRequired();
            builder.Property(w => w.Description).IsRequired().HasMaxLength(1000);
            builder.Property(w => w.DateTimeCreate).IsRequired();
            builder.Property(p => p.UrlImage).IsRequired();
        }
    }

    public class ProductInformationConfiguration : IEntityTypeConfiguration<ProductInformation>
    {
        public void Configure(EntityTypeBuilder<ProductInformation> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.Product)
                .WithOne(w => w.ProductInformation);

            builder.Property(w => w.Memory).IsRequired();
            builder.Property(w => w.NumberOfCores).IsRequired();
            builder.Property(w => w.OperatingSystem).IsRequired().HasMaxLength(200);
            builder.Property(w => w.Ram).IsRequired();
            builder.Property(p => p.ScreenSize).IsRequired().HasMaxLength(10);
        }
    }

    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasMany(w => w.Products)
                .WithOne(w => w.Provider);

            builder.Property(w => w.Name).IsRequired().HasMaxLength(100);
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasMany(w => w.Users)
                .WithOne(w => w.Role);

            builder.Property(w => w.Name).IsRequired().HasMaxLength(10);
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.Basket)
                .WithOne(w => w.User)
                .HasForeignKey<Basket>(w => w.UserId);
            builder.HasOne(w => w.Role)
                .WithMany(w => w.Users)
                .HasForeignKey(w => w.RoleId);
            builder.HasMany(w => w.Assessments)
                .WithOne(w => w.User);
            builder.HasMany(w => w.Orders)
                .WithOne(w => w.User);
            builder.HasMany(w => w.Comments)
                .WithOne(w => w.User);

            builder.Property(w => w.UserName).IsRequired().HasMaxLength(50);
            builder.Property(w => w.Password).IsRequired().HasMaxLength(50);
            builder.Property(w => w.Email).IsRequired().HasMaxLength(100);
            builder.Property(w => w.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(w => w.LastName).IsRequired().HasMaxLength(50);
            builder.Property(w => w.PhoneNumber).IsRequired().HasMaxLength(17);
            builder.Property(w => w.State).IsRequired().HasMaxLength(50);
            builder.Property(w => w.City).IsRequired().HasMaxLength(50);
            builder.Property(w => w.Address).IsRequired().HasMaxLength(50);
            builder.Property(w => w.HouseNumber).IsRequired().HasMaxLength(3);
            builder.Property(w => w.DateTimeCreate).IsRequired();

        }
    }
}