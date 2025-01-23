using TodoApp.Application.Common.Interfaces.Persistence;
using TodoApp.Domain.Menus;
using TodoApp.Infrastructure.Common.Persistence.Repositories;

namespace TodoApp.Infrastructure.Features.Menus.Persistence;

public class MenuRepository : Repository<Menu>, IMenuRepository
{
    public MenuRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<List<Menu>> GetAllAsync()
    {
        return (await base.GetAllAsync()).OrderBy(m => m.Name).ToList();
    }

}