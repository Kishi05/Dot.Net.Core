using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Section25.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<SignInManager<ApplicationUser>>();

builder.Services.AddIdentity<ApplicationUser,ApplicationRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredUniqueChars = 3;
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDBContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole,ApplicationDBContext,Guid>>();

// Global Authorization Policy (in use)
// ------------------------------------
// Automatically requires authentication for all endpoints
// unless explicitly marked with [AllowAnonymous].

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

    options.AddPolicy("NotAuthenticated",policy =>
    {
        policy.RequireAssertion(context =>
        {
            return !context.User.Identity?.IsAuthenticated ?? false;
        });
    });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

var app = builder.Build();

app.UseStaticFiles();

app.MapAreaControllerRoute(
    name: "areas",
    areaName:"Admin",
    pattern: "Admin/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Login}/{id?}"
    );

app.UseAuthentication();
app.UseAuthorization();

app.UseHsts();
app.UseHttpsRedirection();

// ──────────────────────────────────────────────────────────────────────────────
// OPTIONAL ALTERNATIVE: Enforce authentication via **custom middleware**
// -----------------------------------------------------------------------------
// We *can* protect every page by routing requests through our own
// UserAuthMiddleware and skipping only the public paths (e.g., /Home/Login).
// This approach is commented‑out because we’re now using the built‑in
// FallbackPolicy (see AddAuthorization below), which automatically requires
// authenticated users for every endpoint unless [AllowAnonymous] is applied.
//
// To switch back to the middleware method, simply uncomment the block:
//
// app.UseWhen(
//     ctx => !ctx.Request.Path.StartsWithSegments("/Home/Login", StringComparison.OrdinalIgnoreCase) &&
//            !ctx.Request.Path.Equals("/", StringComparison.OrdinalIgnoreCase),
//     branch => branch.UseUserAuthMiddleware());
// ──────────────────────────────────────────────────────────────────────────────


app.Run();
