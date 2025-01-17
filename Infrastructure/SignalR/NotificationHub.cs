using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SignalR
{
    public class NotificationHub:Hub
    {
      
        public override async Task OnConnectedAsync()
        {
            string tenantId =  Context.GetHttpContext()?.Request.Query["tenantId"];
            string userId = Context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(tenantId))
            {
                
                await Groups.AddToGroupAsync(Context.ConnectionId, tenantId);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string tenantId = Context.GetHttpContext()?.Request.Query["tenantId"];

            if (!string.IsNullOrEmpty(tenantId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, tenantId);
            }

            await base.OnDisconnectedAsync(exception);
        }
        public Task SendNotificationToTenant(string tenantId, string message)
        {
            return Clients.Group(tenantId).SendAsync("ReceiveMessage", message);
        }

    }
}
