using Azure.Messaging.ServiceBus;
using FoodShop.Services.User.Api.Services.Contracts.Messaging;
using Microsoft.Extensions.Azure;
using System.Text.Json;

namespace FoodShop.Services.User.Api.Services.Messaging
{
    public sealed class MessagePublisherService : IMessagePublisherService
    {
        private readonly ServiceBusClient _serviceBusClient;

        public MessagePublisherService(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public async Task SendMessageAsync<TMessage>(TMessage message, string publisherName)
        {
            var sender = CreateSender(publisherName);

            var body = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(body);

            await sender.SendMessageAsync(serviceBusMessage);
        }

        private ServiceBusSender CreateSender(string publisherName)
        {
            return _serviceBusClient.CreateSender(publisherName);
        }
    }
}
