using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TodoApp.Domain.Menus;
using TodoApp.Domain.Todos;
using TodoApp.Domain.Todos.Entities;
using TodoApp.Domain.Todos.ValueObjects;

namespace TodoApp.Infrastructure.Features.Todos.Persistence;

public sealed class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        ConfigureTodoTable(builder);
        ConfigureTodoKanbanTable(builder);
    }

    private void ConfigureTodoTable(EntityTypeBuilder<Todo> builder)
    {
        builder
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(t => t.Finished)
            .HasDefaultValue(false);

        builder
            .Property(t => t.FinishedOnUtc);

        builder.ComplexProperty(t => t.DateRange, dateRangeBuilder =>
        {
            dateRangeBuilder
                .Property(t => t.Start)
                .HasColumnName(nameof(DateRange.Start))
                .HasDefaultValue(DateOnly.MinValue);

            dateRangeBuilder
                .Property(t => t.End)
                .HasColumnName(nameof(DateRange.End))
                .HasDefaultValue(DateOnly.MinValue);
        });

        builder
            .Property(t => t.UserId);

        builder
            .HasOne<Menu>()
            .WithMany()
            .HasForeignKey(t => t.MenuId);

        builder
            .Property(t => t.IsDeleted)
            .HasDefaultValue(false);

        builder
            .Property(t => t.TimeRemember)
            .HasDefaultValue(null);

        builder
           .Property(e => e.CreatedOnUtc);

        builder
            .Property(e => e.UpdatedOnUtc);

        builder
            .HasQueryFilter(t => !t.IsDeleted);
    }

    private void ConfigureTodoKanbanTable(EntityTypeBuilder<Todo> builder)
    {
        builder.OwnsMany(t => t.Kanbuns, kanbanBuilder =>
        {
            kanbanBuilder
                .HasKey(nameof(Kanban.Id), "TodoId");

            kanbanBuilder
                .WithOwner()
                .HasForeignKey("TodoId");

            kanbanBuilder
                .Property(k => k.Name);

            kanbanBuilder
                .Property(k => k.Type);
        });

        var element = builder.Metadata
            .FindNavigation(nameof(Todo.Kanbuns))!;

        element.SetField("_kanBanItens");
        element.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}