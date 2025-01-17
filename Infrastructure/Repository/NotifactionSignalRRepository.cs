using Application.Contracts;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class NotifactionSignalRRepository : INotifactionSignalRRepository
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotifactionSignalRRepository(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task SendNotificationToTenant(string tenantId, string message)
        {
            return _hubContext.Clients.Group(tenantId).SendAsync("ReceiveMessage", message);
        }
        public Task SendNotificationToUser(string userId, string message)
        {
            return _hubContext.Clients.Group(userId).SendAsync("ReceiveMessage", message);
        }
    }
}
