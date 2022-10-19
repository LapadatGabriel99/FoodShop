﻿using Azure.Messaging.ServiceBus;
using System.Text.Json;
using System.Text;
using FoodShop.Integration.ServiceBus.Messages;

namespace FoodShop.Services.Email.Api.Services
{
    public sealed class EmailQueueConsumerService : BackgroundService
    {
        private readonly ILogger<EmailQueueConsumerService> _logger;
        private readonly ServiceBusClient _serviceBusClient;

        public EmailQueueConsumerService(ILogger<EmailQueueConsumerService> logger, ServiceBusClient serviceBusClient)
        {
            _logger = logger;
            _serviceBusClient = serviceBusClient;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Message Consumer Service is starting...");

           await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Message Consumer Service is stopping...");

            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var options = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            };

            ServiceBusProcessor processor = default(ServiceBusProcessor);

            try
            {
                processor = _serviceBusClient.CreateProcessor("emailservicequeue", options);

                processor.ProcessMessageAsync += ProcessMessageAsync;
                processor.ProcessErrorAsync += ProcessErrorAsync;

                _logger.LogInformation($"{DateTime.Now}: Starting message processor...");
                await processor.StartProcessingAsync();

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"{DateTime.Now}: Processing requests from emailservicequeue");
                }

                _logger.LogInformation($"{DateTime.Now}: Stopping message processor...");
                await processor.StopProcessingAsync();

                processor.ProcessMessageAsync -= ProcessMessageAsync;
                processor.ProcessErrorAsync -= ProcessErrorAsync;
            }
            catch(Exception ex) when(stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning(ex, "Execution Cancelled");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
            }
            finally
            {
                _logger.LogInformation("Disposing of message processor");
                await processor.DisposeAsync();
            }
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.ErrorSource.ToString());
            _logger.LogDebug(args.FullyQualifiedNamespace);
            _logger.LogDebug(args.EntityPath);
            _logger.LogError(args.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var json = Encoding.UTF8.GetString(args.Message.Body);

            var createEmailMessage = JsonSerializer.Deserialize<CreateEmailMessage>(json);

            await args.CompleteMessageAsync(args.Message);
        }
    }
}