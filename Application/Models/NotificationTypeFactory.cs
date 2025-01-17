using Application.Contracts;
using Application.Services;
using Domain.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
     public class NotificationTypeFactory
    {
        private readonly INotifactionSignalRRepository _notificationRepository;
        private readonly INotificationDatabaseRepository  _notificationDatabaseRepository;
        
        public NotificationTypeFactory(INotifactionSignalRRepository notificationRepository, INotificationDatabaseRepository notificationDatabaseRepository)
        {
            _notificationRepository = notificationRepository;
            _notificationDatabaseRepository = notificationDatabaseRepository;
         
        }
        public  INotificationSender GetNotificationChannel(CommunicationChannels channel)
        {
            if(channel == CommunicationChannels.RealTimeNotification)
            {
                return new RealTimeNotificationService(_notificationRepository , _notificationDatabaseRepository);
            }else if(channel == CommunicationChannels.Email)
            {
                return new EmailNotificationService(_notificationDatabaseRepository);
            }else if (channel == CommunicationChannels.SMS)
            {
                return new SMSNotificationService(_notificationDatabaseRepository);
            }
            return new RealTimeNotificationService(_notificationRepository , _notificationDatabaseRepository);
        }
    }
}
