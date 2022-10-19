namespace FoodShop.Services.User.Api.Services.Contracts.Messaging
{
    public interface IMessagePublisherService
    {
        Task SendMessageAsync<TMessage>(TMessage message, string publisherName);
    }
}
