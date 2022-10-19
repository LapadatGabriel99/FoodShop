using Azure.Messaging.ServiceBus;
using FoodShop.Services.Email.Api.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAzureClients(config =>
{
    config.AddServiceBusClient(builder.Configuration.GetConnectionString("FoodShopServiceBus"))
        .WithName("FoodShopServiceBus");
});

builder.Services.AddHostedService(provider =>
{
    var azureServiceBusClientFactory = provider.GetService<IAzureClientFactory<ServiceBusClient>>();
    var foodShopServiceBusClient = azureServiceBusClientFactory.CreateClient("FoodShopServiceBus");
    var logger = provider.GetService<ILogger<EmailQueueConsumerService>>();

    return new EmailQueueConsumerService(logger, foodShopServiceBusClient);
});

builder.Services.Configure<HostOptions>(options =>
{
    options.ShutdownTimeout = TimeSpan.FromSeconds(30);
    options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
