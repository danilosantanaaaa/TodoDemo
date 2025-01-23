using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace TodoApp.Application.Common.Models;

public class PaginatedList<TItem>
{
    public List<TItem> Items { get; private set; } = new List<TItem>();
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }
    
    [JsonConstructor]
    public PaginatedList() { }

    private PaginatedList(
        List<TItem> items,
        int count,
        int pageNumber,
        int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<TItem>> CreateAsync(
        IQueryable<TItem> source,
        int pageNumber,
        int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<TItem>(items, count, pageNumber, pageSize);
    }

    public static PaginatedList<TItem> Create(
        List<TItem> source,
        int pageNumber,
        int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<TItem>(items, count, pageNumber, pageSize);
    }

    public static PaginatedList<TItem> Create(
        List<TItem> items,
        int pageNumber = 0,
        int pageSize = 0, 
        bool paginated = false)
    {
        if (paginated) return Create(items, pageNumber, pageSize);

        return new PaginatedList<TItem>(items, items.Count(), pageNumber, pageSize);
    }
}