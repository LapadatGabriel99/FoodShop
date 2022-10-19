using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using FoodShop.Web.Product.Services;
using FoodShop.Web.Product.Services.Contracts;
using FoodShop.Web.Product.Services.Contracts.Filters;
using FoodShop.Web.Product.Services.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IIdentityServerApiService, IdentityServerApiService>();

builder.Services.AddHttpClient<IIdentityServerApiService, IdentityServerApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:IdentityServerApi"]);
});

builder.Services.AddScoped<JwtTokenHeaderHandlerService>();

builder.Services.AddScoped<IProductApiService, ProductApiService>();

builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductManagementApi"]);
})
    .AddHttpMessageHandler<JwtTokenHeaderHandlerService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IGenericResponseHandlerService, GenericResponseHandlerService>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
        config.Cookie.MaxAge = TimeSpan.FromHours(3.5);
        config.ExpireTimeSpan = TimeSpan.FromHours(3
            );
        config.AccessDeniedPath = "/Authorization/Index";
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = builder.Configuration["ServiceUrls:IdentityServerApi"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "foodShop-Product-Management-Admin";
        options.ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A";
        options.ResponseType = "code";
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("foodShop.product.read");
        options.Scope.Add("foodShop.product.create");
        options.Scope.Add("foodShop.product.delete");
        options.Scope.Add("foodShop.product.update");
        options.Scope.Add("offline_access");
        options.ClaimActions.MapUniqueJsonKey("role", "role");
        options.ClaimActions.MapUniqueJsonKey("name", "name");
        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
