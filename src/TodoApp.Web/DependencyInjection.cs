using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;
using MudBlazor.Services;


using TodoApp.Web.Components.Account;
using TodoApp.Web.Services;
using TodoApp.Web.Services.Interfaces;

namespace TodoApp.Web;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddProblemDetails();
        services
            .AddMudBlazorComponents()
            .AddHttpServices()
            .AddIdentity();

        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        return services;
    }

    private static IServiceCollection AddMudBlazorComponents(this IServiceCollection services)
    {
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 3000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });


        return services;
    }

    private static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddScoped<IMenusService, MenusService>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IDashboardService, DashboardService>();

        return services;
    }
}