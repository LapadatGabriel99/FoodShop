using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using FoodShop.Services.Email.Worker.Models;
using FoodShop.Services.Email.Worker.Services;
using FoodShop.Services.Email.Worker.Services.Contracts;
using Microsoft.Extensions.Azure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<EmailOptions>(options => 
        {
            options.From = hostContext.Configuration["EmailOptions:From"];
            options.MailFrom = hostContext.Configuration["EmailOptions:MailFrom"];
        });

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

        services.AddSingleton<EmailClient>(provider => new EmailClient(hostContext.Configuration.GetConnectionString("FoodShopCommunicationServices")));

        services.AddSingleton<IEmailSenderService, EmailSenderService>();


        services.AddHostedService<EmailQueueConsumerService>(provider =>
        {
            var serviceBusClient = provider.GetService<ServiceBusClient>();
            var logger = provider.GetService<ILogger<EmailQueueConsumerService>>();
            var emailSenderService = provider.GetService<IEmailSenderService>();

            return new EmailQueueConsumerService(logger, serviceBusClient, hostContext.Configuration, emailSenderService);
        });

        services.Configure<HostOptions>(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(60);
        });
    })
    .Build();

await host.RunAsync();
