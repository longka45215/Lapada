using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WEB_Shop_PRN.Models;

public partial class WebAppOrderContext : DbContext
{
    public WebAppOrderContext()
    {
    }

    public WebAppOrderContext(DbContextOptions<WebAppOrderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("WebApp_Order"));
        // getConnectionString: giống với file json 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__26A1118D51657C91");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("addressID");
            entity.Property(e => e.AddressDetail)
                .HasMaxLength(100)
                .HasColumnName("addressDetail");
            entity.Property(e => e.CityId)
                .HasMaxLength(50)
                .HasColumnName("cityID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("customerName");
            entity.Property(e => e.DistrictId)
                .HasMaxLength(50)
                .HasColumnName("districtID");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("userPassword");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Address__cityID__37A5467C");

            entity.HasOne(d => d.District).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK__Address__distric__38996AB5");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__415B03D8A038249F");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cartID");
            entity.Property(e => e.ProductAmount).HasColumnName("productAmount");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__productID__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__userID__403A8C7D");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CityId)
                .HasMaxLength(50)
                .HasColumnName("cityID");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .HasColumnName("cityName");
            entity.Property(e => e.CitySlug)
                .HasMaxLength(30)
                .HasColumnName("citySlug");
            entity.Property(e => e.CityType)
                .HasMaxLength(30)
                .HasColumnName("cityType");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District");

            entity.Property(e => e.DistrictId)
                .HasMaxLength(50)
                .HasColumnName("districtID");
            entity.Property(e => e.CityId)
                .HasMaxLength(50)
                .HasColumnName("cityID");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .HasColumnName("districtName");
            entity.Property(e => e.DistrictType)
                .HasMaxLength(30)
                .HasColumnName("districtType");

            entity.HasOne(d => d.City).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_District_City");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__0809337D334F6464");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.AddressId).HasColumnName("addressID");
            entity.Property(e => e.OrderStatus).HasColumnName("orderStatus");
            entity.Property(e => e.OrderTotalPrice).HasColumnName("orderTotalPrice");
            entity.Property(e => e.ProductAmount).HasColumnName("productAmount");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Order__addressID__3C69FB99");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Order__productID__3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Order__userID__3B75D760");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__2D10D14AB89C9D10");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(200)
                .HasColumnName("productDescription");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("productImage");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("productName");
            entity.Property(e => e.ProductNumber).HasColumnName("productNumber");
            entity.Property(e => e.ProductPrice).HasColumnName("productPrice");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CDF23374B93");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("userPassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
