using Microsoft.AspNetCore.Identity;

using TodoApp.Infrastructure.Common.Identity;

namespace TodoApp.Web.Components.Account;

internal sealed class IdentityUserAccessor(
    UserManager<ApplicationIdentityUser> userManager,
    IdentityRedirectManager redirectManager)
{
    public async Task<ApplicationIdentityUser> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus(
                "Account/InvalidUser",
                $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.",
                context);
        }

        return user;
    }
}