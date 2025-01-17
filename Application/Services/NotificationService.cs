
using Application.Contracts;
using Application.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
         private readonly NotificationTypeFactory _notificationTypeFactory;
         private readonly INotifactionSignalRRepository _notifactionRepository;
        public NotificationService(NotificationTypeFactory notificationTypeFactory, INotifactionSignalRRepository notifactionRepository)
        {
            _notificationTypeFactory = notificationTypeFactory;
            _notifactionRepository = notifactionRepository;
        }
        //from message broker //
        public  void  HandleNotificationMessage(string type ,string message)
        {
             CommunicationChannels parsedType;
             Enum.TryParse(type, out parsedType);
            _notificationTypeFactory.GetNotificationChannel(parsedType).Send(message);
        }

     


        // From API // 
        public async Task SendRealTimeNotification(RealTimeNotification notification)
        {
           await _notifactionRepository.SendNotificationToTenant(notification.TenantId, notification.Message);
        }
    }
}
