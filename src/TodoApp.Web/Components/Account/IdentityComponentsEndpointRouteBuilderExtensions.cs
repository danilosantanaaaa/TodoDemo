using Microsoft.AspNetCore.Identity;

using TodoApp.Infrastructure.Common.Identity;

namespace TodoApp.Web.Components.Account;

internal static class IdentityComponentsEndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        var accountGroup = endpoints.MapGroup("/Account");

        accountGroup.MapGet("/Logout", async (SignInManager<ApplicationIdentityUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return TypedResults.LocalRedirect($"~/Account/Login");
        });

        return accountGroup;
    }
}