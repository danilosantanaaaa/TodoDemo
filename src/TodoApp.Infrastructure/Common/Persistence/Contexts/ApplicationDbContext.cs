using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using TodoApp.Domain.Common.Interfaces;
using TodoApp.Domain.Menus;
using TodoApp.Domain.Todos;
using TodoApp.Infrastructure.Common.Identity;

namespace TodoApp.Infrastructure.Common.Persistence.Contexts;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationIdentityUser>(options), IUnitOfWork
{
    public DbSet<Menu> Menus => null!;
    public DbSet<Todo> Todos => null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker
            .Entries<IAuditable>()
            .ToList();

        foreach (var entry in entities)
        {
            if (entry.State == EntityState.Added)
            {
                SetCurrentPropertyValue(
                    entry,
                    nameof(IAuditable.CreatedOnUtc),
                    DateTime.Now);
            }

            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                SetCurrentPropertyValue(
                    entry,
                    nameof(IAuditable.UpdatedOnUtc),
                    DateTime.Now);
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    private void SetCurrentPropertyValue(
        EntityEntry entry,
        string propertyName,
        DateTime now)
    {
        entry.Property(propertyName).CurrentValue = now;
    }
}