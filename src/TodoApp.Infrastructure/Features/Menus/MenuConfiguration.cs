using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TodoApp.Domain.Menus;

namespace TodoApp.Infrastructure.Features.Menus.Persistence;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Icon)
            .IsRequired();

        builder.HasData(
            new
            {
                Id = Guid.Parse("3a9815f0-018a-41de-8be9-f6dc99e9c632"),
                Name = "Meu dia",
                Icon = "Sun",
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            },

            new
            {
                Id = Guid.Parse("9724094d-da9c-4939-ba44-ad5851f61bfe"),
                Name = "Objetivos",
                Icon = "Plan",
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            },

            new
            {
                Id = Guid.Parse("a70ec313-92fa-4965-b1a3-4760b015a03e"),
                Name = "Metas",
                Icon = "Alarm",
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            }
        );

        builder
            .Property(m => m.IsDeleted)
            .HasDefaultValue(false);

        builder
            .HasQueryFilter(x => !x.IsDeleted);

        builder
            .Property(e => e.CreatedOnUtc);

        builder
            .Property(e => e.UpdatedOnUtc);
    }
}