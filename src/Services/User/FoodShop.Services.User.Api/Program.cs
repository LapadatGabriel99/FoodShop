using FoodShop.Services.User.Api;
using FoodShop.Services.User.Api.Converters;
using FoodShop.Services.User.Api.Converters.Contracts;
using FoodShop.Services.User.Api.Data;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Filters;
using FoodShop.Services.User.Api.Filters.Contracts;
using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Services;
using FoodShop.Services.User.Api.Services.Contracts;
using FoodShop.Services.User.Api.Specification;
using FoodShop.Services.User.Api.Specification.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<NotUserThatRequestedSpecification>();
builder.Services.AddTransient<Func<SpecificationEnum, ISpecification<UserModel>>>(serviceProvider => key =>
{
    switch (key)
    {
        case SpecificationEnum.NotUserThatRequestedSpecification:
            return serviceProvider.GetService<NotUserThatRequestedSpecification>();
        
        default:
            throw new NotImplementedException();
    }
});
builder.Services.AddTransient<UserFilter>();
builder.Services.AddTransient<Func<FilterEnum, IFilter<UserModel>>>(serviceProvider => key =>
{
    switch (key)
    {
        case FilterEnum.UserModel:
            return serviceProvider.GetService<UserFilter>();

        default:
            throw new NotImplementedException();
    }
});
builder.Services.AddTransient<IUserFilterService, UserFilterService>();

builder.Services.AddScoped<IConverter<UserModelDto, UserModel>, UserModelDtoToUserModelConverter>();
builder.Services.AddScoped<IConverter<UserModel, UserModelDto>, UserModelToUserModelDtoConverter>();
builder.Services.AddScoped<IObjectConverterService, ObjectConverterService>();
builder.Services.AddScoped<IUserConverterService, UserConverterService>();
builder.Services.AddScoped<IUserService, UserService>();

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User.Read.Admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "foodShop.user.read");
        policy.RequireRole(Role.UserMangementAdmin);
    });
    options.AddPolicy("User.Read", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "foodShop.user.read");
    });
    options.AddPolicy("User.Create.Admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "foodShop.user.create");
        policy.RequireRole(Role.UserMangementAdmin);
    });
    options.AddPolicy("User.Update", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "foodShop.user.update");
    });
    options.AddPolicy("User.Delete.Admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "foodShop.user.delete");
        policy.RequireRole(Role.UserMangementAdmin);
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodShop.Services.User.Api" });
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
