using Microsoft.EntityFrameworkCore;
using ZadanieDomowe7.Models;

namespace ZadanieDomowe7.Data;

public class AppDbContext : DbContext
{
    public DbSet<PC> PCs { get; set; } = null!;
    public DbSet<Component> Components { get; set; } = null!;
    public DbSet<ComponentType> ComponentTypes { get; set; } = null!;
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; } = null!;
    public DbSet<PCComponent> PCComponents { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure ComponentType
        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Abbreviation).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Configure ComponentManufacturer
        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Abbreviation).IsRequired().HasMaxLength(10);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FoundationDate).IsRequired();
        });

        // Configure Component
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Code);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            
            entity.HasOne(e => e.ComponentManufacturer)
                .WithMany(m => m.Components)
                .HasForeignKey(e => e.ComponentManufacturersId);
            
            entity.HasOne(e => e.ComponentType)
                .WithMany(t => t.Components)
                .HasForeignKey(e => e.ComponentTypeId);
        });

        // Configure PC
        modelBuilder.Entity<PC>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Weight).HasPrecision(10, 2);
            entity.Property(e => e.Warranty).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Stock).IsRequired();
        });

        // Configure PCComponent
        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.HasKey(e => new { e.PCId, e.ComponentCode });
            
            entity.HasOne(e => e.PC)
                .WithMany(p => p.PCComponents)
                .HasForeignKey(e => e.PCId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(e => e.ComponentCode)
                .HasPrincipalKey(c => c.Code)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed ComponentTypes
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "RAM", Name = "Memory" },
            new ComponentType { Id = 3, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 4, Abbreviation = "SSD", Name = "Solid State Drive" },
            new ComponentType { Id = 5, Abbreviation = "PSU", Name = "Power Supply Unit" }
        );

        // Seed ComponentManufacturers
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "Intel", FullName = "Intel Corporation", FoundationDate = new DateTime(1968, 7, 18) },
            new ComponentManufacturer { Id = 2, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
            new ComponentManufacturer { Id = 3, Abbreviation = "Kingston", FullName = "Kingston Technology", FoundationDate = new DateTime(1987, 10, 17) },
            new ComponentManufacturer { Id = 4, Abbreviation = "Corsair", FullName = "Corsair Components", FoundationDate = new DateTime(1994, 6, 15) },
            new ComponentManufacturer { Id = 5, Abbreviation = "Samsung", FullName = "Samsung Electronics", FoundationDate = new DateTime(1969, 3, 1) }
        );

        // Seed Components
        modelBuilder.Entity<Component>().HasData(
            new Component { Code = "CPU001", Name = "Intel Core i9-13900K", Description = "High-end desktop processor", ComponentManufacturersId = 1, ComponentTypeId = 1 },
            new Component { Code = "CPU002", Name = "AMD Ryzen 9 7950X", Description = "High-performance desktop CPU", ComponentManufacturersId = 2, ComponentTypeId = 1 },
            new Component { Code = "RAM001", Name = "Kingston Fury 32GB DDR5", Description = "Gaming memory module", ComponentManufacturersId = 3, ComponentTypeId = 2 },
            new Component { Code = "RAM002", Name = "Corsair Vengeance 16GB DDR5", Description = "Professional memory", ComponentManufacturersId = 4, ComponentTypeId = 2 },
            new Component { Code = "GPU001", Name = "NVIDIA RTX 4090", Description = "Flagship graphics card", ComponentManufacturersId = 1, ComponentTypeId = 3 },
            new Component { Code = "GPU002", Name = "AMD Radeon RX 7900 XTX", Description = "High-end gaming GPU", ComponentManufacturersId = 2, ComponentTypeId = 3 },
            new Component { Code = "SSD001", Name = "Samsung 990 Pro 2TB", Description = "NVMe SSD for gaming", ComponentManufacturersId = 5, ComponentTypeId = 4 },
            new Component { Code = "SSD002", Name = "Corsair MP600 1TB", Description = "Fast NVMe storage", ComponentManufacturersId = 4, ComponentTypeId = 4 },
            new Component { Code = "PSU001", Name = "Corsair RM1000e 1000W", Description = "Gold modular PSU", ComponentManufacturersId = 4, ComponentTypeId = 5 }
        );

        // Seed PCs
        modelBuilder.Entity<PC>().HasData(
            new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5m, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
            new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2m, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
            new PC { Id = 3, Name = "Workstation Ultra", Weight = 15.8m, Warranty = 60, CreatedAt = new DateTime(2026, 5, 1, 10, 15, 0), Stock = 3 },
            new PC { Id = 4, Name = "Server Standard", Weight = 20.0m, Warranty = 60, CreatedAt = new DateTime(2026, 4, 20, 14, 45, 0), Stock = 2 }
        );

        // Seed PCComponents
        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "CPU001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "RAM001", Amount = 2 },
            new PCComponent { PCId = 1, ComponentCode = "GPU001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "SSD001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "PSU001", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "CPU002", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "RAM002", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "SSD002", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "CPU001", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "RAM001", Amount = 4 },
            new PCComponent { PCId = 3, ComponentCode = "GPU002", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "SSD001", Amount = 2 },
            new PCComponent { PCId = 4, ComponentCode = "CPU002", Amount = 2 },
            new PCComponent { PCId = 4, ComponentCode = "RAM002", Amount = 4 },
            new PCComponent { PCId = 4, ComponentCode = "SSD002", Amount = 2 }
        );
    }
}

