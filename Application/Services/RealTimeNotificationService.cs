using Application.Contracts;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RealTimeNotificationService : INotificationSender
    {
     private readonly INotifactionSignalRRepository _notifactionRepository;
      private readonly INotificationDatabaseRepository notificationDatabaseRepository;
        public RealTimeNotificationService(INotifactionSignalRRepository notifactionRepository, INotificationDatabaseRepository notificationDatabaseRepository)
        {
            _notifactionRepository = notifactionRepository;
            this.notificationDatabaseRepository = notificationDatabaseRepository;
        }
        public Task Send (string message)
        {
            var notification = JsonConvert.DeserializeObject<RealTimeNotification>(message);
            notificationDatabaseRepository.InsertNotification(notification);
            return _notifactionRepository.SendNotificationToTenant(notification.TenantId, notification.Message);

        }
    }
}
