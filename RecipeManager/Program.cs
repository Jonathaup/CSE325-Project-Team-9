using Microsoft.EntityFrameworkCore;
using RecipeManager.Data;
using RecipeManager.Components;
using RecipeManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services for Blazor and Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure the database context to use SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register RecipeService as a scoped service
builder.Services.AddScoped<RecipeService>();

var app = builder.Build();

// Configure request pipeline for production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Redirect HTTP to HTTPS and enable anti-forgery
app.UseHttpsRedirection();
app.UseAntiforgery();

// Map static files and Blazor components
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Run the application
app.Run();
