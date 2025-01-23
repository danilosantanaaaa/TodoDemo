using Microsoft.EntityFrameworkCore;

using TodoApp.Application;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.Common.Persistence.Contexts;
using TodoApp.Web;
using TodoApp.Web.Components;
using TodoApp.Web.Components.Account;
using TodoApp.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

// Add services to the container.
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapTodoHubEndpoints();

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseExceptionHandler();

app.UseStatusCodePagesWithRedirects("/Error/404");

app.MapRazorComponents<App>()
       .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

{
    using var dbContext = app.Services.GetRequiredService<ApplicationDbContext>();
    if (dbContext is not null)
    {
        dbContext.Database.Migrate();
    }

    dbContext?.Dispose();
}

await app.RunAsync();