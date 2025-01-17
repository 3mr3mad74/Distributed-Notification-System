using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Notification_Service.Controllers
{
    [Route("api/[controller]")]

    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpPost("send-notificaion")]
        public async Task<IActionResult> SendRealTimeNotification(RealTimeNotification notification)
        {
            await _notificationService.SendRealTimeNotification(notification);
            return Ok();
        }

        

    }
}
