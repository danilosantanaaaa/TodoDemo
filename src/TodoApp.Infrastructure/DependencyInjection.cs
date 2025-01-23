using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TodoApp.Application.Common.Interfaces.Persistence;
using TodoApp.Infrastructure.Common.Identity;
using TodoApp.Infrastructure.Common.Services;
using TodoApp.Infrastructure.Features.Dashboards.Persistence;
using TodoApp.Infrastructure.Features.Menus.Persistence;
using TodoApp.Infrastructure.Features.Todos.Persistence;
using TodoApp.Infrastructure.Features.Todos.Services;

namespace TodoApp.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddPersistence(configuration)
            .AddIdentity();

        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        }).AddIdentityCookies();

        services.AddIdentityCore<ApplicationIdentityUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<ITodoServiceSignalR, TodoServiceSignalR>();
        services.AddSignalR();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("PostgreSQL")));

        services.AddScoped<IUnitOfWork>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();

        return services;
    }
}