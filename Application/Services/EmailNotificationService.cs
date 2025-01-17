using Application.Contracts;
using Application.Models;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace Application.Services
{
    public class EmailNotificationService : INotificationSender
    {
        private readonly INotificationDatabaseRepository _notificationDatabaseRepository;
        private  SendGridSettings _sendGridSettings;

        public EmailNotificationService(INotificationDatabaseRepository notificationDatabaseRepository)
        {
            _notificationDatabaseRepository = notificationDatabaseRepository;
        }
        public async  Task Send(string  message)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
           .Build();
           _sendGridSettings = configuration.GetSection("SendGrid").Get<SendGridSettings>();
            var notification = JsonConvert.DeserializeObject<EmailNotification>(message);
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            var from = new EmailAddress(notification.From, _sendGridSettings.SenderName);
            var to = new EmailAddress(notification.To);
            var emailMessage = MailHelper.CreateSingleEmail(from, to,notification.Subject, plainTextContent: null, htmlContent: notification.Message);
            var response = await client.SendEmailAsync(emailMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send email. Status Code: {response.StatusCode}");
            }
            await  _notificationDatabaseRepository.InsertNotification(notification);

        }
    }
}
