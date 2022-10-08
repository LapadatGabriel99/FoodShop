using FoodShop.Services.Product.Api.Authorization.Filters.Exceptions;
using FoodShop.Services.Product.Api.Converters.Categories;
using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Converters.Products;
using FoodShop.Services.Product.Api.Data;
using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Extensions;
using FoodShop.Services.Product.Api.Models;
using FoodShop.Services.Product.Api.Repository;
using FoodShop.Services.Product.Api.Repository.Categories;
using FoodShop.Services.Product.Api.Repository.Contracts;
using FoodShop.Services.Product.Api.Repository.Contracts.Categories;
using FoodShop.Services.Product.Api.Repository.Contracts.Products;
using FoodShop.Services.Product.Api.Repository.Products;
using FoodShop.Services.Product.Api.Services.Authorization;
using FoodShop.Services.Product.Api.Services.Cateogries;
using FoodShop.Services.Product.Api.Services.Contracts.Authorization;
using FoodShop.Services.Product.Api.Services.Contracts.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Converters;
using FoodShop.Services.Product.Api.Services.Contracts.Converters.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Converters.Products;
using FoodShop.Services.Product.Api.Services.Contracts.Products;
using FoodShop.Services.Product.Api.Services.Contracts.Seeder;
using FoodShop.Services.Product.Api.Services.Contracts.Transaction;
using FoodShop.Services.Product.Api.Services.Converters;
using FoodShop.Services.Product.Api.Services.Converters.Categories;
using FoodShop.Services.Product.Api.Services.Converters.Products;
using FoodShop.Services.Product.Api.Services.Products;
using FoodShop.Services.Product.Api.Services.Seeder;
using FoodShop.Services.Product.Api.Services.Transaction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDatabaseTransactionService<ApplicationDbContext>, DatabaseTransactionService>();

builder.Services.AddScoped<IConverter<ProductDto, Product>, ProductDtoToProductConverter>();
builder.Services.AddScoped<IConverter<Product, ProductDto>, ProductToProductDtoConverter>();
builder.Services.AddScoped<IConverter<CategoryDto, Category>, CategoryDtoToCategoryConverter>();
builder.Services.AddScoped<IConverter<Category, CategoryDto>, CategoryToCategoryDtoConverter>();
builder.Services.AddScoped<IObjectConverterService, ObjectConverterService>();
builder.Services.AddScoped<IProductConverterService, ProductConverterService>();
builder.Services.AddScoped<ICategoryConverterService, CategoryConverterService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDbSeederService, DbSeederService>();

// Add services to the container.
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:44336/";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodShop.Services.Product.Api" });
    c.EnableAnnotations();
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.MigrateDatabase<ApplicationDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
