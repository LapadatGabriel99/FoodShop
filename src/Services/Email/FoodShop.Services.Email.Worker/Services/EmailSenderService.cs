using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using FoodShop.Services.Email.Worker.Models;
using FoodShop.Services.Email.Worker.Services.Contracts;
using Microsoft.Extensions.Options;

namespace FoodShop.Services.Email.Worker.Services
{
    internal class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly EmailClient _emailClient;

        public EmailSenderService(ILogger<EmailSenderService> logger, IOptions<EmailOptions> emailOptions, EmailClient emailClient)
        {
            _logger = logger;
            _emailOptions = emailOptions;
            _emailClient = emailClient;
        }

        public async Task<SentEmailResult> SendEmailAsync(EmailModel email)
        {
            var emailContent = new EmailContent(email.Subject);
            emailContent.PlainText = email.Body;

            var emailAddresses = email.Recipients.Select(address => new EmailAddress(address));
            var emailRecipients = new EmailRecipients(emailAddresses);

            var emailMessage = new EmailMessage(_emailOptions.Value.From, emailContent, emailRecipients);
            var emailResult = await _emailClient.SendAsync(emailMessage, CancellationToken.None);

            _logger.LogInformation(emailResult.Value.MessageId);

            return new SentEmailResult { MessageId = emailResult.Value.MessageId };
        }

        public async Task<bool> GetSendStatusAsync(SentEmailResult emailResult)
        {
            var messageStatus = await _emailClient.GetSendStatusAsync(emailResult.MessageId);
            _logger.LogInformation($"MessageStatus = {messageStatus.Value.Status}");

            TimeSpan duration = TimeSpan.FromMinutes(5);
            long start = DateTime.Now.Ticks;
            var success = false;

            do
            {
                messageStatus = await _emailClient.GetSendStatusAsync(emailResult.MessageId);
                if (messageStatus.Value.Status != SendStatus.Queued)
                {
                    success = true;
                    _logger.LogInformation($"MessageStatus = {messageStatus.Value.Status}");
                    break;
                }
                Thread.Sleep(10000);
                _logger.LogInformation($"...");

            } while (DateTime.Now.Ticks - start < duration.Ticks);

            return success;
        }
    }
}
