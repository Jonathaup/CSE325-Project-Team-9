using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using RecipeManager.Components;
using RecipeManager.Data;
using RecipeManager.Services;

var builder = WebApplication.CreateBuilder(args);


// ==========================================
// 1. Service Registration
// ==========================================

// Register Controller services. This is required for AuthController to work.
builder.Services.AddControllers();

// Add services for Blazor and Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Authentication (Cookie-based)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "RecipeAuth";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

// Add Authorization & Cascading State (Required for <AuthorizeView> to work in Blazor)
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Configure the database context to use SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Application Logic Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();  // or db.Database.Migrate();
}

// Configure request pipeline for production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


// Redirect HTTP to HTTPS and enable anti-forgery
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Enable Authentication & Authorization (Must be before mapping endpoints)
app.UseAuthentication();
app.UseAuthorization();

// Map static files and Blazor components
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map the AuthController (Must be mapped for the Login form POST to work)
app.MapControllers();

// --- NEW: Fallback route for SPA / client-side routes ---
app.MapFallback(context =>
{
    context.Response.Redirect("/"); // redirect unknown routes to root
    return Task.CompletedTask;
});


// Run the application
app.Run();
