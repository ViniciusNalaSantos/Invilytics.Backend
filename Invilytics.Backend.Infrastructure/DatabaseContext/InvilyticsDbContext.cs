using Invilytics.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invilytics.Backend.Infrastructure.DatabaseContext;
public class InvilyticsDbContext : DbContext
{
    public InvilyticsDbContext(DbContextOptions<InvilyticsDbContext> options) : base(options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    public DbSet<Users> Users { get; set; }
    public DbSet<InvestmentSectors> InvestmentSectors { get; set; }
    public DbSet<InvestmentTypes> InvestmentTypes { get; set; }
    public DbSet<Portfolios> Portfolios { get; set; }
    public DbSet<Purchases> Purchases { get; set; }
    public DbSet<Sales> Sales { get; set; }
    public DbSet<StockQuotesHistory> StockQuotesHistory { get; set; }
    public DbSet<Stocks> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ModelUsers(modelBuilder);
        ModelInvestmentSectors(modelBuilder);
        ModelInvestmentTypes(modelBuilder);
        ModelPortfolios(modelBuilder);
        ModelPurchases(modelBuilder);
        ModelSales(modelBuilder);
        ModelStockQuotesHistory(modelBuilder);
        ModelStocks(modelBuilder);
    }

    private void ModelUsers(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<Users>();

        entityModelBuilder
            .ToTable("USERS")
            .HasKey(l => l.Id);

        entityModelBuilder
            .HasIndex(l => l.PublicId)
            .IsUnique();
        entityModelBuilder
            .Property(l => l.PublicId)
            .HasColumnName("ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Name)
            .HasColumnName("NAME")
            .HasMaxLength(100)
            .IsRequired();
        
        entityModelBuilder
            .HasIndex(l => l.Email)
            .IsUnique();
        entityModelBuilder
            .Property(l => l.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(255)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.PasswordHash)
            .HasColumnName("PASSWORD_HASH")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.CreatedAt)
            .HasColumnName("CREATED_AT")
            .IsRequired();
    }

    private void ModelInvestmentSectors(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<InvestmentSectors>();

        entityModelBuilder
            .ToTable("INVESTMENT_SECTORS")
            .HasKey(l => l.Id);

        entityModelBuilder
            .Property(l => l.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(100)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.InvestmentTypeId)
            .HasColumnName("INVESTMENT_TYPE_ID")
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.User)
            .WithMany(l => l.InvestmentSectors)
            .HasForeignKey(l => l.UserId)
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.InvestmentTypes)
            .WithMany(l => l.InvestmentSectors)
            .HasForeignKey(l => l.InvestmentTypeId)
            .IsRequired();
    }

    private void ModelInvestmentTypes(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<InvestmentTypes>();

        entityModelBuilder
            .ToTable("INVESTMENT_TYPES")
            .HasKey(l => l.Id);

        entityModelBuilder
            .Property(l => l.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(50)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.User)
            .WithMany(l => l.InvestmentTypes)
            .HasForeignKey(l => l.UserId)
            .IsRequired();
    }

    private void ModelPortfolios(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<Portfolios>();

        entityModelBuilder
            .ToTable("PORTFOLIOS")
            .HasKey(l => new { l.UserId, l.StockId });

        entityModelBuilder
            .Property(l => l.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.StockId)
            .HasColumnName("STOCK_ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.ValueInvested)
            .HasColumnName("VALUE_INVESTED")
            .HasPrecision(19, 6)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Quantity)
            .HasColumnName("QUANTITY")
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.User)
            .WithMany(l => l.Portfolios)
            .HasForeignKey(l => l.UserId);

        entityModelBuilder
            .HasOne(l => l.Stock)
            .WithMany(l => l.Portfolios)
            .HasForeignKey(l => l.StockId);
    }

    private void ModelPurchases(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<Purchases>();

        entityModelBuilder
            .ToTable("PURCHASES")
            .HasKey(l => l.Id);

        entityModelBuilder
            .Property(l => l.StockId)
            .HasColumnName("STOCK_ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Price)
            .HasColumnName("PRICE")
            .HasPrecision(19, 6)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.PurchaseTimestamp)
            .HasColumnName("PURCHASE_TIMESTAMP")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Quantity)
            .HasColumnName("QUANTITY")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.User)
            .WithMany(l => l.Purchases)
            .HasForeignKey(l => l.UserId);

        entityModelBuilder
            .HasOne(l => l.Stock)
            .WithMany(l => l.Purchases)
            .HasForeignKey(l => l.StockId);
    }

    private void ModelSales(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<Sales>();

        entityModelBuilder
            .ToTable("SALES")
            .HasKey(l => l.Id);

        entityModelBuilder
            .Property(l => l.StockId)
            .HasColumnName("STOCK_ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Price)
            .HasColumnName("PRICE")
            .HasPrecision(19, 6)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Quantity)
            .HasColumnName("QUANTITY")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.SaleTimestamp)
            .HasColumnName("SALE_TIMESTAMP")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Quantity)
            .HasColumnName("QUANTITY")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.User)
            .WithMany(l => l.Sales)
            .HasForeignKey(l => l.UserId);

        entityModelBuilder
            .HasOne(l => l.Stock)
            .WithMany(l => l.Sales)
            .HasForeignKey(l => l.StockId);
    }

    private void ModelStockQuotesHistory(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<StockQuotesHistory>();

        entityModelBuilder
            .ToTable("STOCK_QUOTES_HISTORY")
            .HasKey(l => l.Id);

        entityModelBuilder
            .Property(l => l.StockId)
            .HasColumnName("STOCK_ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Price)
            .HasColumnName("PRICE")
            .HasPrecision(19, 6)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.User)
            .WithMany(l => l.StockQuotesHistories)
            .HasForeignKey(l => l.UserId);

        entityModelBuilder
            .HasOne(l => l.Stock)
            .WithMany(l => l.StockQuotesHistory)
            .HasForeignKey(l => l.StockId);
    }

    private void ModelStocks(ModelBuilder modelBuilder)
    {
        var entityModelBuilder = modelBuilder.Entity<Stocks>();

        entityModelBuilder
            .ToTable("STOCKS")
            .HasKey(l => l.Id);

        entityModelBuilder
            .Property(l => l.Symbol)
            .HasColumnName("SYMBOL")
            .HasMaxLength(25)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(150)
            .IsRequired();

        entityModelBuilder
            .Property(l => l.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        entityModelBuilder
            .Property(l => l.InvestmentSectorId)
            .HasColumnName("INVESTMENT_SECTOR_ID")
            .IsRequired();

        entityModelBuilder
            .HasOne(l => l.User)
            .WithMany(l => l.Stocks)
            .HasForeignKey(l => l.UserId);

        entityModelBuilder
            .HasOne(l => l.InvestmentSector)
            .WithMany(l => l.Stocks)
            .HasForeignKey(l => l.InvestmentSectorId);
    }
}
