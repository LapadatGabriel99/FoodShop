using Azure.Messaging.ServiceBus;
using FoodShop.Services.Email.Worker;
using FoodShop.Services.Email.Worker.Services;
using Microsoft.Extensions.Azure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddAzureClients(config =>
        {
            config.AddServiceBusClient(hostContext.Configuration.GetConnectionString("FoodShopServiceBus"))
                .WithName("FoodShopServiceBus");
        });

        services.AddSingleton<ServiceBusClient>(provider =>
        {
            var azureServiceBusClientFactory = provider.GetService<IAzureClientFactory<ServiceBusClient>>();
            return azureServiceBusClientFactory.CreateClient("FoodShopServiceBus");
        });

        services.AddHostedService<EmailQueueConsumerService>(provider =>
        {
            var serviceBusClient = provider.GetService<ServiceBusClient>();
            var logger = provider.GetService<ILogger<EmailQueueConsumerService>>();

            return new EmailQueueConsumerService(logger, serviceBusClient, hostContext.Configuration);
        });

        services.Configure<HostOptions>(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(60);
        });
    })
    .Build();

await host.RunAsync();
