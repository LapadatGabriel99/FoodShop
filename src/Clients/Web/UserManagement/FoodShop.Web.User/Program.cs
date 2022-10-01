using FoodShop.Web.User.Services;
using FoodShop.Web.User.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<JwtTokenHeaderHandlerService>();

builder.Services.AddHttpClient("IdentityServerApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:IdentityServerApi"]);
});

builder.Services.AddScoped<IUserApiService, UserApiService>();

builder.Services.AddHttpClient<IUserApiService, UserApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:UserManagementApi"]);
})
    .AddHttpMessageHandler<JwtTokenHeaderHandlerService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
        config.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        config.AccessDeniedPath = "/Authorization/Index";
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = builder.Configuration["ServiceUrls:IdentityServerApi"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "foodShop-User-Management-Admin";
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.ResponseType = "code";
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("foodShop.user.read");
        options.Scope.Add("foodShop.user.create");
        options.Scope.Add("foodShop.user.delete");
        options.Scope.Add("foodShop.user.update");
        options.Scope.Add("offline_access");
        options.ClaimActions.MapUniqueJsonKey("role", "role");
        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
