using BlazorWebAppCamilla.Components;
using BlazorWebAppCamilla.Components.Account;
using BlazorWebAppCamilla.Data;
using BlazorWebAppCamilla.Services;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<ApplicationUserRepository>();
builder.Services.AddScoped<UserAddressRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<UserDataService>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();


builder.Services.ConfigureApplicationCookie(x =>
{
    //webbläsaren kan inte komma åt cookien, minimerar risk för crossside-scripting
    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("SuperAdmins", policy => policy.RequireRole("SuperAdmin"));
    x.AddPolicy("CIO", policy => policy.RequireRole("SuperAdmin", "CIO"));
    x.AddPolicy("Admins", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin"));
    x.AddPolicy("Users", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "User"));
});

builder.Services.AddSingleton(s => new GraphQLHttpClient("https://courseprovider-silicon-camilla.azurewebsites.net/api/graphql?code=LL18Cb5IbCut7M9hw0bCm5RFI7hOjOJwHOmU6-Dyd1TXAzFuSGQ4FA%3D%3D", new SystemTextJsonSerializer()));
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebAppCamilla.Client._Imports).Assembly);
app.MapAdditionalIdentityEndpoints();



using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = ["SuperAdmin", "CIO", "Admin", "User"];
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
