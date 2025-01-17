using Application.Contracts;
using Application.Models;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using Twilio.Types;

namespace Application.Services
{
    public class SMSNotificationService : INotificationSender
    {
        private readonly INotificationDatabaseRepository _notificationDatabaseRepository;
        private TwilioSettings twilioSettings;
        public SMSNotificationService(INotificationDatabaseRepository notificationDatabaseRepository)
        {
            _notificationDatabaseRepository = notificationDatabaseRepository;
        }
        public async Task Send(string message)
        {
            var notification = JsonConvert.DeserializeObject<SMSNotification>(message);
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();
            twilioSettings = configuration.GetSection("Twilio").Get<TwilioSettings>();

            TwilioClient.Init(twilioSettings.AccountSid, twilioSettings.AuthToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber(notification.ToPhoneNumber));
            messageOptions.From = new PhoneNumber(twilioSettings.PhoneNumber);
            messageOptions.Body = notification.Message; 
            MessageResource.Create(messageOptions);
            await _notificationDatabaseRepository.InsertNotification(notification);
        }
    }
}
