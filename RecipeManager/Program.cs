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

// Add Razor/Blazor services
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

// Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Application Logic Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ==========================================
// 2. Middleware Pipeline
// ==========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// Changed from UseStaticAssets() to UseStaticFiles() to serve wwwroot files correctly
app.UseStaticFiles();

// Enable Anti-forgery tokens
app.UseAntiforgery();

// Enable Authentication & Authorization (Must be before mapping endpoints)
app.UseAuthentication();
app.UseAuthorization();

// ==========================================
// 3. Endpoint Mapping
// ==========================================

// Map the Blazor App
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map the AuthController (Must be mapped for the Login form POST to work)
app.MapControllers();

app.Run();