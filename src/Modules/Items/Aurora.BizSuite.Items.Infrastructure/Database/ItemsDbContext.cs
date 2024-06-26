﻿namespace Aurora.BizSuite.Items.Infrastructure.Database;

public sealed class ItemsDbContext(
    DbContextOptions<ItemsDbContext> options)
    : DbContext(options), IUnitOfWork
{
    #region DbSet properties

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<ItemUnit> ItemUnits { get; set; } = null!;
    public DbSet<UnitOfMeasurement> Units { get; set; } = null!;

    #endregion

    internal const string DEFAULT_SCHEMA = "Items";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemsDbContext).Assembly);
    }
}