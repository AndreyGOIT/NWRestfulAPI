using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NWRestfulAPI.Models;

public partial class NorthwindOriginalContext : DbContext
{
    public NorthwindOriginalContext()
    {
    }

    public NorthwindOriginalContext(DbContextOptions<NorthwindOriginalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; }

    public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }

    public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }

    public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-U8H2C3L\\SQLEXCAREERIA5; Database=NorthwindOriginal;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
        });

        modelBuilder.Entity<CustomerAndSuppliersByCity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Customer and Suppliers by City");

            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.Relationship)
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CustomerCustomerDemo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CustomerCustomerDemo");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CustomerTypeID");

            entity.HasOne(d => d.Customer).WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCustomerDemo_Customers");
        });

        modelBuilder.Entity<CustomerDemographic>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CustomerDesc).HasColumnType("ntext");
            entity.Property(e => e.CustomerTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CustomerTypeID");
        });

        modelBuilder.Entity<EmployeeTerritory>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.TerritoryId)
                .HasMaxLength(20)
                .HasColumnName("TerritoryID");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Region");

            entity.Property(e => e.RegionDescription)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.HomePage).HasColumnType("ntext");
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.TerritoryDescription)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.TerritoryId)
                .HasMaxLength(20)
                .HasColumnName("TerritoryID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
