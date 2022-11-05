using FoodShop.Services.Email.Worker.Models;

namespace FoodShop.Services.Email.Worker.Services.Contracts
{
    internal interface IEmailSenderService
    {
        Task<SentEmailResult> SendEmailAsync(EmailModel email);

        Task<bool> GetSendStatusAsync(SentEmailResult emailResult);
    }
}
