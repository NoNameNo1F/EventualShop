﻿using Application.EventSourcing.EventStore.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventSourcing.EventStore.Contexts;

public class EventStoreDbContext : DbContext
{
    public EventStoreDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<OrderStoreEvent> OrderStoreEvents { get; set; }
    public DbSet<OrderSnapshot> OrderSnapshots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder
            .Properties<string>()
            .AreUnicode(false)
            .HaveMaxLength(1024);
}